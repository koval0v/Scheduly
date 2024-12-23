using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data_access.Entities;
using Data_access.Interfaces;
using TeacherService.Dtos;

namespace Business.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        private readonly IMapper _mapper;

        private readonly IDisciplineTeacherService _disciplineTeacherService;

        private readonly HttpClient _httpClient;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, HttpClient httpClient,
            IDisciplineTeacherService disciplineTeacherService)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _disciplineTeacherService = disciplineTeacherService;
        }

        public async Task<TeacherModel> AddAsync(TeacherModel model)
        {
            var teacher = _mapper.Map<Teacher>(model);

            var teacherCreated = await _teacherRepository.AddAsync(teacher);

            await _teacherRepository.SaveAsync();

            return _mapper.Map<TeacherModel>(teacherCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _teacherRepository.DeleteByIdAsync(modelId);

            await _teacherRepository.SaveAsync();
        }

        public async Task<IEnumerable<TeacherModel>> GetAllAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TeacherModel>>(teachers);
        }

        public async Task<TeacherModel> GetByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            return _mapper.Map<TeacherModel>(teacher);
        }

        public async Task<IEnumerable<TeacherModel>> GetTeachersByFacultyId(int facultyId)
        {
            var facultyDisciplines = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<DisciplineDto>>(_httpClient.BaseAddress + "facultyDisciplines/" + facultyId);

            List<TeacherModel> facultyTeachers = new List<TeacherModel>();

            if (facultyDisciplines is null)
                return facultyTeachers;

            foreach (var discipline in facultyDisciplines)
            {
                var teachersOfDiscipline = await _disciplineTeacherService.GetTeachersIdByDisciplineIdAsync(discipline.Id);
                foreach (var teacher in teachersOfDiscipline)
                {
                    facultyTeachers.Add(teacher);
                }
            }

            return facultyTeachers;
        }

        public async Task UpdateAsync(int id, TeacherModel model)
        {
            var teacher = _mapper.Map<Teacher>(model);

            await Task.Run(()=> _teacherRepository.Update(teacher));

            await _teacherRepository.SaveAsync();
        }
    }
}
