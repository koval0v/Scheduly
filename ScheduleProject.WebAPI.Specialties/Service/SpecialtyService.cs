using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data_access.Entities;
using Data_access.Interfaces;

namespace Business.Service
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _SpecialtyRepository;

        private readonly IMapper _mapper;

        public SpecialtyService(ISpecialtyRepository SpecialtyRepository, IMapper mapper)
        {
            _SpecialtyRepository = SpecialtyRepository;
            _mapper = mapper;
        }

        public async Task<SpecialtyModel> AddAsync(SpecialtyModel model)
        {
            var Specialty = _mapper.Map<Specialty>(model);

            var SpecialtyCreated = await _SpecialtyRepository.AddAsync(Specialty);

            await _SpecialtyRepository.SaveAsync();

            return _mapper.Map<SpecialtyModel>(SpecialtyCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _SpecialtyRepository.DeleteByIdAsync(modelId);

            await _SpecialtyRepository.SaveAsync();
        }

        public async Task<IEnumerable<SpecialtyModel>> GetAllAsync()
        {
            var specialties = await _SpecialtyRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SpecialtyModel>>(specialties);
        }

        public async Task<SpecialtyModel> GetByIdAsync(int id)
        {
            var Specialty = await _SpecialtyRepository.GetByIdAsync(id);

            return _mapper.Map<SpecialtyModel>(Specialty);
        }

        public async Task UpdateAsync(int id, SpecialtyModel model)
        {
            var Specialty = _mapper.Map<Specialty>(model);

            await Task.Run(() => _SpecialtyRepository.Update(Specialty));

            await _SpecialtyRepository.SaveAsync();
        }
    }
}
