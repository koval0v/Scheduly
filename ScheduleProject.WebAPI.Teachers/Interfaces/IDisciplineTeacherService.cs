using Business.Models;

namespace Business.Interfaces
{
    public interface IDisciplineTeacherService
    {
        Task<DisciplineTeacherModel> AddAsync(DisciplineTeacherModel model);
        Task<IEnumerable<DisciplineTeacherModel>> GetAllAsync();
        Task<IEnumerable<TeacherModel>> GetTeachersIdByDisciplineIdAsync(int disciplineId);
        Task<IEnumerable<TeacherModel>> GetLecturersIdByDisciplineIdAsync(int disciplineId);
        Task<IEnumerable<TeacherModel>> GetPracticiansIdByDisciplineIdAsync(int disciplineId);
        Task DeleteByIdAsync(int modelId);
    }
}
