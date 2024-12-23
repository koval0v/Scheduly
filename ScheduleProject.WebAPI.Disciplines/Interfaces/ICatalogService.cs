using DisciplineService.Models;

namespace DisciplineService.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogModel> AddAsync(CatalogModel model);
        Task<IEnumerable<CatalogModel>> GetAllAsync();
        Task<CatalogModel> GetByIdAsync(int id);
        Task DeleteByIdAsync(int modelId);
    }
}
