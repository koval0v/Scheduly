using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data_access.Entities;
using Data_access.Interfaces;
using SpecialtyService.Models;

namespace Business.Service
{
    public class FacultySpecialtyService : IFacultySpecialtyService
    {
        private readonly IFacultySpecialtyRepository _SpecialtyRepository;

        private readonly IMapper _mapper;

        public FacultySpecialtyService(IFacultySpecialtyRepository SpecialtyRepository, IMapper mapper)
        {
            _SpecialtyRepository = SpecialtyRepository;
            _mapper = mapper;
        }

        public async Task<FacultySpecialtyModel> AddAsync(FacultySpecialtyModel model)
        {
            var facultySpecialty = _mapper.Map<FacultySpecialty>(model);

            var facultySpecialtyCreated = await _SpecialtyRepository.AddAsync(facultySpecialty);

            await _SpecialtyRepository.SaveAsync();

            return _mapper.Map<FacultySpecialtyModel>(facultySpecialtyCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _SpecialtyRepository.DeleteByIdAsync(modelId);

            await _SpecialtyRepository.SaveAsync();
        }

        public async Task<IEnumerable<FacultySpecialtyModel>> GetAllAsync()
        {
            var facultySpecialties = await _SpecialtyRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<FacultySpecialtyModel>>(facultySpecialties);
        }

        public async Task<FacultySpecialtyModel> GetByIdAsync(int id)
        {
            var facultySpecialty = await _SpecialtyRepository.GetByIdAsync(id);

            return _mapper.Map<FacultySpecialtyModel>(facultySpecialty);
        }

        public async Task<IEnumerable<SpecialtyModel>> GetSpecialtiesByFacultyIdAsync(int facultyId)
        {
            var facultySpecialties = await _SpecialtyRepository.GetAllAsync();

            return facultySpecialties.Where(x => x.FacultyId == facultyId).Select(p => _mapper.Map<SpecialtyModel>(p.Specialty));
        }

        public async Task UpdateAsync(int id, FacultySpecialtyModel model)
        {
            var Specialty = _mapper.Map<FacultySpecialty>(model);

            await Task.Run(() => _SpecialtyRepository.Update(Specialty));

            await _SpecialtyRepository.SaveAsync();
        }
    }
}
