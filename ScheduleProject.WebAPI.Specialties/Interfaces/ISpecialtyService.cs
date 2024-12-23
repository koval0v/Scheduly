using Business.Models;

namespace Business.Interfaces
{
    public interface ISpecialtyService
    {
        Task<SpecialtyModel> AddAsync(SpecialtyModel model);
        Task<IEnumerable<SpecialtyModel>> GetAllAsync();
        Task<SpecialtyModel> GetByIdAsync(int id);
        Task UpdateAsync(int id, SpecialtyModel model);
        Task DeleteByIdAsync(int modelId);
    }
}
