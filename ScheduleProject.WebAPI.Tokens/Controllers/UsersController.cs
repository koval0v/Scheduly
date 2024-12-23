using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenService.Extentions;
using TokenService.Interfaces;
using TokenService.Models;
using TokenService.RabbitMQModels;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userAccountService;
        private readonly IEIService _eiService;
        private readonly IBus _busService;

        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userAccountService, IEIService eiService, IBus busService,
            ILogger<UsersController> logger)
        {
            _userAccountService = userAccountService;
            _eiService = eiService;
            _busService = busService;

            _logger = logger;
        }

        [HttpPost("addUserToEIEmail")]
        public async Task<string> SendAddUserToEIEmailTemplate(AddUserToEIEmailTemplate emailData)
        {
           if (emailData is not null)
            {
                Uri uri = new Uri("rabbitmq://localhost/addUserToEIQueue");
                var endpoint = await _busService.GetSendEndpoint(uri);
                await endpoint.Send(emailData);
                return "true";
            }

            return "false";
        }

        /// <summary>
        /// Generating JWT token.
        /// </summary>
        /// <param name="model">The LoginModel</param>
        /// <returns></returns>
        /// <response code="200">Returns the JWT token</response>
        /// <response code="400">Password is incorrect. </response>
        /// <response code="404">The user not found. </response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<string>> Token(LoginRequest model)
        {
            var token = await _userAccountService.GetTokenAsync(model);

            var loginedId = await _userAccountService.GetIdByEmailAsync(model.Email);

            var user = await _userAccountService.GetByIdAsync(loginedId);

            var allUserEIs = await _eiService.GetAllUserEIAsync();

            List<EIModel> accepted = new List<EIModel>();

            foreach(var userEI in user.EIs)
            {
                if (allUserEIs.FirstOrDefault(p => p.UserId == user.Id && p.EIId == userEI.Id).IsAccepted)
                {
                    accepted.Add(userEI);
                }
            }

            return Ok(new { access_token = token, id = loginedId, eis = accepted });
        }

        /// <summary>
        /// Gets a list of all UserModel.
        /// </summary>
        /// <returns>The list of all UserModel. </returns>
        /// <response code="200">Returns the list of all UserModel.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllAsync()
        {
            _logger.LogInformation("UsersController GetAll K8S", DateTime.UtcNow);

            var users = await _userAccountService.GetAllAsync();

            return Ok(users);
        }

        /// <summary>
        /// Gets a list of all RoleModel.
        /// </summary>
        /// <returns>The list of all RoleModel. </returns>
        /// <response code="200">Returns the list of all RoleModel.</response>
        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoleModel>))]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetAllRolesAsync()
        {
            var roles = await _userAccountService.GetAllRolesAsync();

            return Ok(roles);
        }

        /// <summary>
        /// Gets the UserModel by identifier.
        /// </summary>
        /// <returns>The UserModel. </returns>
        /// <response code="200">Returns the UserModel.</response>
        /// <response code="404">The user not found. </response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var user = await _userAccountService.GetByIdAsync(id);

            return Ok(user);
        }

        /// <summary>
        /// Gets a list of UserModel by role.
        /// </summary>
        /// <param name="id">The role identifier.</param>
        /// <returns>The list of UserModel. </returns>
        /// <response code="200">Returns the list of UserModel.</response>
        /// <response code="404">The role not found. </response>
        [HttpGet("roles/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<UserModel>> GetByRole(int id)
        {
            var user = await _userAccountService.GetByRoleAsync(id);

            return Ok(user);
        }

        /// <summary>
        /// Changes the user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The role or the user not found. </response>
        [HttpPut("{userId}/role/{roleId}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> ChangeUserRole(int userId, int roleId)
        {
            await _userAccountService.ChangeRoleAsync(userId, roleId);

            return NoContent();
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The RegistrationModel.</param>
        /// <returns></returns>
        /// <response code="201">Returns the created UserModel. </response>
        /// <response code="400">The email is already used. </response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<int>> Register([FromBody] RegistrationModel model)
        {
            var user = await _userAccountService.RegisterAsync(model);

            return CreatedAtAction(nameof(Register), user.Id);
        }

        [HttpPost("ei")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EIModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<EIModel>> AddEI([FromBody] EIModel model)
        {
            model.Link = GenerateLink(model.Name);

            var ei = await _eiService.AddAsync(model);

            return CreatedAtAction(nameof(Register), ei);
        }

        [HttpGet("ei/{name}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<bool>> EINameIsUnique(string name)
        {
            var eis = await _eiService.GetAllAsync();

            var isUniqueName = !eis.Any(p => p.Name == name);

            return Ok(isUniqueName);
        }

        [HttpGet("ei")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<EIModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<EIModel>>> GetAllEis()
        {
            var eis = await _eiService.GetAllAsync();

            return Ok(eis);
        }

        [HttpGet("eiById/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EIModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<EIModel>> GetEiById(int id)
        {
            var eis = await _eiService.GetAllAsync();

            return Ok(eis.FirstOrDefault(o => o.Id == id));
        }

        [HttpGet("eiMembers/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<UserModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllMembersByEiId(int id)
        {
            var eis = await _eiService.GetAllAsync();

            var ei = eis.FirstOrDefault(o => o.Id == id);

            var eiUsers = await _eiService.GetAllUserEIAsync();

            List<UserModel> users = new List<UserModel>();

            foreach (var eiUser in eiUsers)
            {
                if (eiUser.EIId == id)
                {
                    var u = await _userAccountService.GetByIdAsync(eiUser.UserId);
                    users.Add(u);
                }
            }

            return Ok(users);
        }

        [HttpPost("userEi")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EIModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<UserEIModel>> AddUserEI([FromBody] UserEIModel model)
        {
            var ei = await _eiService.AddUserEIAsync(model);

            return CreatedAtAction(nameof(Register), ei);
        }

        [HttpGet("userEi")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<EIModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<UserEIModel>> GetAllUserEIs()
        {
            var userEis = await _eiService.GetAllUserEIAsync();

            return Ok(userEis);
        }

        [HttpPut("userEi/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Update(int id, UserEIModel model)
        {
            await _eiService.UpdateUserEIAsync(id, model);

            return NoContent();
        }

        [HttpGet("eiMembersFull/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<EIModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<UserEIFullDto>> GetAllUserFullInfoEIs(int id)
        {
            var userEis = await _eiService.GetAllUserEIAsync();

            var EIusersEis = userEis.Where(o => o.EIId == id);

            List<UserEIFullDto> users = new List<UserEIFullDto>();

            foreach (var i in EIusersEis)
            {
                UserEIFullDto user = new UserEIFullDto()
                {
                    EIId = i.EIId,
                    Id = i.Id,
                    IsAnswered = i.IsAnswered,
                    IsAccepted = i.IsAccepted,
                    IsAdmin = i.IsAdmin,
                    UserId = i.UserId,
                    User = await _userAccountService.GetByIdAsync(i.UserId)
                };

                users.Add(user);
            }

            return Ok(users);
        }

        [HttpDelete("userEi/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> DeleteUserEI(int id)
        {
            await _eiService.DeleteUserEIByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The user not found. </response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userAccountService.DeleteByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Updates the specified UserModel.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The UserModel.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="400">The nickaname is already taken. </response>
        /// <response code="400">The emails are different. </response>
        /// <response code="404">The user not found. </response>
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> Update(int id, [FromBody] UserModel model)
        {
            await _userAccountService.UpdateAsync(id, model);

            return NoContent();
        }

        private string GenerateLink(string eiName)
        {
            return eiName.Trim().ToLower();
        }
    }
}
