using AutoMapper;
using DisciplineService.Dtos;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using DisciplineService.Models;

namespace DisciplineService.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineRepository _disciplineRepository;

        private readonly IMapper _mapper;

        private readonly ISpecialtyDisciplineService _specialtyDisciplineService;

        private readonly HttpClient _httpClient;

        public DisciplineService(IDisciplineRepository disciplineRepository, IMapper mapper, HttpClient httpClient,
            ISpecialtyDisciplineService specialtyDisciplineService)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _specialtyDisciplineService = specialtyDisciplineService;
        }

        public async Task<DisciplineModel> AddAsync(DisciplineModel model)
        {
            var discipline = _mapper.Map<Discipline>(model);

            var disciplineCreated = await _disciplineRepository.AddAsync(discipline);

            await _disciplineRepository.SaveAsync();

            return _mapper.Map<DisciplineModel>(disciplineCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _disciplineRepository.DeleteByIdAsync(modelId);

            await _disciplineRepository.SaveAsync();
        }

        public async Task<IEnumerable<DisciplineModel>> GetAllAsync()
        {
            var disciplines = await _disciplineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DisciplineModel>>(disciplines);
        }

        public async Task<DisciplineModel> GetByIdAsync(int id)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(id);

            return _mapper.Map<DisciplineModel>(discipline);
        }

        public async Task<IEnumerable<DisciplineModel>> GetSelective()
        {
            var disciplines = await _disciplineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DisciplineModel>>(disciplines.Where(p => p.IsSelective));
        }

        public async Task<IEnumerable<DisciplineModel>> GetMandatory()
        {
            var disciplines = await _disciplineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DisciplineModel>>(disciplines.Where(p => !p.IsSelective));
        }

        public async Task<IEnumerable<DisciplineModel>> GetDisciplinesByFacultyId(int facultyId)
        {
            var facultySpecialties = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<SpecialtyDto>>(_httpClient.BaseAddress + "facultySpecialties/" + facultyId);

            List<DisciplineModel> facultyDisciplines = new List<DisciplineModel>();

            foreach (var specialty in facultySpecialties)
            {
                var disciplinesOfSpecialty = await _specialtyDisciplineService.GetDisciplinesBySpecialtyIdAsync(specialty.Id);
                foreach (var discipline in disciplinesOfSpecialty)
                {
                    facultyDisciplines.Add(discipline);
                }
            }

            return facultyDisciplines;
        }

        public async Task UpdateAsync(int id, DisciplineModel model)
        {
            var discipline = _mapper.Map<Discipline>(model);

            await Task.Run(() => _disciplineRepository.Update(discipline));

            await _disciplineRepository.SaveAsync();
        }
    }
}
