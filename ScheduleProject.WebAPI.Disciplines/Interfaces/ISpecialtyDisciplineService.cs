using DisciplineService.Models;

namespace DisciplineService.Interfaces
{
    public interface ISpecialtyDisciplineService
    {
        Task<SpecialtyDisciplineModel> AddAsync(SpecialtyDisciplineModel model);
        Task<IEnumerable<SpecialtyDisciplineModel>> GetAllAsync();
        Task<IEnumerable<DisciplineModel>> GetDisciplinesBySpecialtyIdAsync(int specialtyId);
        Task<IEnumerable<DisciplineModel>> GetDisciplinesBySpecialtyIdAndSemesterAsync(int specialtyId, int semester);
        Task DeleteByIdAsync(int modelId);
    }
}
