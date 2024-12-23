using TokenService.Entities;

namespace TokenService.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> AddAsync(Role entity);
        Task<Role> DeleteByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        void Update(Role entity);
    }
}