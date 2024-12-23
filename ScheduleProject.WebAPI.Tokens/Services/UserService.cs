using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Validation.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TokenService.Entities;
using TokenService.Interfaces;
using TokenService.Models;
using TokenService.Repositories;

namespace TokenService.Services
{
    public class UserService : IUserService
    { 
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The authentication options
        /// </summary>
        private readonly IOptions<JwtOptions> _authOptions;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtOptions> authOptions)
        {  
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authOptions = authOptions;
        }

        public async Task<string> GetTokenAsync(LoginRequest login)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(login.Email);

            //var user = new User() { Email = "string@gmail.com", Credentials = new Credentials() { Role = new Role() { RoleName = "Admin" } } };

            if (user == null)
            {
                throw new Exception(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", login.Email?.ToString()));
            }

            if (!VerifyPassword(login.Password, user.Credentials.PasswordHash, user.Credentials.PasswordSalt))
            {
                throw new WrongPasswordException(ExceptionMessages.WrongPassword);
            }

            return GenerateToken(user);
        }

        public async Task<int> GetIdByEmailAsync(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            return user.Id;
        }

        public async Task ChangeRoleAsync(int userId, int roleId)
        {
            var credentials = await _unitOfWork.CredentialsRepository.GetByUserIdAsync(userId);
            if (credentials == null)
            {
                throw new Exception(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", userId.ToString()));
            }

            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);

            if (role == null)
            {
                throw new Exception(String.Format(ExceptionMessages.NotFound, typeof(Role).Name, "Id", roleId.ToString()));
            }

            credentials.Role = role;
            credentials.RoleId = role.Id;

            _unitOfWork.CredentialsRepository.Update(credentials);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Deletes the Account by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="Exception"></exception>
        public async Task DeleteByIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            await _unitOfWork.UserRepository.DeleteByIdAsync(userId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        /// <summary>
        /// Gets the User by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;UserModel&gt;
        /// </returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", id.ToString()));
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetByRoleAsync(int roleId)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);

            if (role == null)
            {
                throw new Exception(String.Format(ExceptionMessages.NotFound, typeof(Role).Name, "Id", roleId.ToString()));
            }

            var users = await _unitOfWork.UserRepository.GetAllAsync();

            users = users?.Where(u => u.Credentials.RoleId == roleId);

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task<UserModel> RegisterAsync(RegistrationModel authModel)
        {
            bool isExist = await _unitOfWork.UserRepository.IsEmailExistAsync(authModel.Email);

            if (isExist)
            {
                throw new InvalidRegistrationException(String.Format(ExceptionMessages.EmailUsed, authModel.Email));
            }

            var user = CreateAccount(authModel, (int)BasicRoles.User);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateAsync(int id, UserModel userModel)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", userModel.Email.ToString()));
            }

            if (user.Email != userModel.Email)
            {
                throw new DifferenceEmailException(String.Format(ExceptionMessages.DifferenceEmail, user.Email, id));
            }

            user = _mapper.Map(userModel, user);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }

        private User CreateAccount(RegistrationModel registrationModel, int roleId)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registrationModel.Password));
            }

            User user = new User
            {
                Email = registrationModel.Email,
                Credentials = new Credentials
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = roleId
                }
            };

            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string GenerateToken(User user)
        {
            var authParams = _authOptions.Value;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Credentials.Role.RoleName)
            };
            claims.Add(new Claim("id", user.Id.ToString()));

            var key = authParams.GetSymmetricSecurityKey();
            var credantials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credantials);

            return new JwtSecurityTokenHandler().WriteToken(token);

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_authOptions.Value.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }
    }  
}
