using DisciplineService.Models;

namespace DisciplineService.Interfaces
{
    public interface ICatalogDisciplineService
    {
        Task<CatalogDisciplineModel> AddAsync(CatalogDisciplineModel model);
        Task<IEnumerable<CatalogDisciplineModel>> GetAllAsync();
        Task<IEnumerable<DisciplineModel>> GetDisciplinesByCatalogIdAsync(int catalogId);
        Task DeleteByIdAsync(int modelId);
    }
}
