using AutoMapper;
using Microsoft.Extensions.Options;
using TokenService.Entities;
using TokenService.Interfaces;
using TokenService.Models;

namespace TokenService.Services
{
    public class EIService : IEIService
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

        public EIService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authOptions = authOptions;
        }

        public async Task<IEnumerable<EIModel>> GetAllAsync()
        {
            var eis = await _unitOfWork.EIRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EIModel>>(eis);
        }

        public async Task<EIModel> AddAsync(EIModel model)
        { 
            var ei = _mapper.Map<EI>(model);

            var eiCreated = await _unitOfWork.EIRepository.AddAsync(ei);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<EIModel>(eiCreated);
        }

        public async Task<UserEIModel> AddUserEIAsync(UserEIModel model)
        {
            var userEI = _mapper.Map<UserEI>(model);

            var userEICreated = await _unitOfWork.EIRepository.AddUserEIAsync(userEI);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserEIModel>(userEICreated);
        }

        public async Task<IEnumerable<UserEIModel>> GetAllUserEIAsync()
        {
            var userEiModels = await _unitOfWork.EIRepository.GetAllUserEIAsync();

            return _mapper.Map<IEnumerable<UserEIModel>>(userEiModels);
        }

        public async Task DeleteUserEIByIdAsync(int userId)
        {
            var user = await _unitOfWork.EIRepository.DeleteUserEIByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            await _unitOfWork.EIRepository.DeleteUserEIByIdAsync(userId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserEIAsync(int id, UserEIModel model)
        {
            var userEI = _mapper.Map<UserEI>(model);

            await Task.Run(() => _unitOfWork.EIRepository.UpdateUserEI(userEI));

            await _unitOfWork.SaveAsync();
        }
    }
}
