using Business.Models;
using SpecialtyService.Models;

namespace Business.Interfaces
{
    public interface IFacultySpecialtyService
    {
        Task<FacultySpecialtyModel> AddAsync(FacultySpecialtyModel model);
        Task<IEnumerable<FacultySpecialtyModel>> GetAllAsync();
        Task<FacultySpecialtyModel> GetByIdAsync(int id);
        Task<IEnumerable<SpecialtyModel>> GetSpecialtiesByFacultyIdAsync(int facultyId);
        Task UpdateAsync(int id, FacultySpecialtyModel model);
        Task DeleteByIdAsync(int modelId);
    }
}
