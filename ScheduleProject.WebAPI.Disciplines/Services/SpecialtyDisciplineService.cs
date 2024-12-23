using AutoMapper;

using DisciplineService.Entities;
using DisciplineService.Interfaces;
using DisciplineService.Models;

namespace CatalogDiscipline.Services
{
    public class SpecialtyDisciplineService : ISpecialtyDisciplineService
    {
        private readonly ISpecialtyDisciplineRepository _specialtyDisciplineRepository;

        private readonly IMapper _mapper;

        public SpecialtyDisciplineService(ISpecialtyDisciplineRepository specialtyDisciplineRepository, IMapper mapper)
        {
            _specialtyDisciplineRepository = specialtyDisciplineRepository;
            _mapper = mapper;
        }

        public async Task<SpecialtyDisciplineModel> AddAsync(SpecialtyDisciplineModel model)
        {
            var specialtyDiscipline = _mapper.Map<SpecialtyDiscipline>(model);

            var specialtyDisciplineeCreated = await _specialtyDisciplineRepository.AddAsync(specialtyDiscipline);

            await _specialtyDisciplineRepository.SaveAsync();

            return _mapper.Map<SpecialtyDisciplineModel>(specialtyDisciplineeCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _specialtyDisciplineRepository.DeleteByIdAsync(modelId);

            await _specialtyDisciplineRepository.SaveAsync();
        }

        public async Task<IEnumerable<SpecialtyDisciplineModel>> GetAllAsync()
        {
            var specialtyDisciplines = await _specialtyDisciplineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SpecialtyDisciplineModel>>(specialtyDisciplines);
        }

        public async Task<IEnumerable<DisciplineModel>> GetDisciplinesBySpecialtyIdAsync(int specialtyId)
        {
            var specialtyDisciplines = await _specialtyDisciplineRepository.GetAllAsync();

            return specialtyDisciplines.Where(x => x.SpecialtyId == specialtyId).Select(p => _mapper.Map<DisciplineModel>(p.Discipline));
        }

        public async Task<IEnumerable<DisciplineModel>> GetDisciplinesBySpecialtyIdAndSemesterAsync(int specialtyId, int semester)
        {
            var specialtyDisciplines = await _specialtyDisciplineRepository.GetAllAsync();

            return specialtyDisciplines.Where(x => x.SpecialtyId == specialtyId && x.Semester == semester).Select(p => _mapper.Map<DisciplineModel>(p.Discipline));
        }

    }
}
