using DisciplineService.Dtos;
using DisciplineService.Models;

namespace DisciplineService.Interfaces
{
    public interface IGroupService
    {
        Task<GroupModel> AddAsync(GroupModel model);
        Task<IEnumerable<GroupModel>> GetAllAsync();
        Task<GroupModel> GetByIdAsync(int id);
        Task DeleteByIdAsync(int modelId);
        Task UpdateAsync(int id, GroupModel model);
    }
}
