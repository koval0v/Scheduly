using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data_access.Entities;
using Data_access.Interfaces;

namespace Business.Service
{
    public class DisciplineTeacherService : IDisciplineTeacherService
    {
        private readonly IDisciplineTeacherRepository _disciplineTeacherRepository;


        private readonly IMapper _mapper;

        public DisciplineTeacherService(IDisciplineTeacherRepository teacherRepository, IMapper mapper)
        {
            _disciplineTeacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<DisciplineTeacherModel> AddAsync(DisciplineTeacherModel model)
        {
            var disciplineTeacher = _mapper.Map<DisciplineTeacher>(model);

            var disciplineTeacherCreated = await _disciplineTeacherRepository.AddAsync(disciplineTeacher);

            await _disciplineTeacherRepository.SaveAsync();

            return _mapper.Map<DisciplineTeacherModel>(disciplineTeacherCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _disciplineTeacherRepository.DeleteByIdAsync(modelId);

            await _disciplineTeacherRepository.SaveAsync();
        }

        public async Task<IEnumerable<DisciplineTeacherModel>> GetAllAsync()
        {
            var catalogDisciplines = await _disciplineTeacherRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DisciplineTeacherModel>>(catalogDisciplines);
        }

        public async Task<IEnumerable<TeacherModel>> GetTeachersIdByDisciplineIdAsync(int disciplineId)
        {
            var catalogDisciplines = await _disciplineTeacherRepository.GetAllAsync();

            return catalogDisciplines.Where(x => x.DisciplineId == disciplineId).Select(p => _mapper.Map<TeacherModel>(p.Teacher));
        }

        public async Task<IEnumerable<TeacherModel>> GetLecturersIdByDisciplineIdAsync(int disciplineId)
        {
            var catalogDisciplines = await _disciplineTeacherRepository.GetAllAsync();

            return catalogDisciplines.Where(x => x.DisciplineId == disciplineId && x.isLecturer).Select(p => _mapper.Map<TeacherModel>(p.Teacher));
        }

        public async Task<IEnumerable<TeacherModel>> GetPracticiansIdByDisciplineIdAsync(int disciplineId)
        {
            var catalogDisciplines = await _disciplineTeacherRepository.GetAllAsync();

            return catalogDisciplines.Where(x => x.DisciplineId == disciplineId && !x.isLecturer).Select(p => _mapper.Map<TeacherModel>(p.Teacher));        }
    }
}
