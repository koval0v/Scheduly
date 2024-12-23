using TokenService.Entities;

namespace TokenService.Interfaces
{
    public interface ICredentialsRepository
    {
        Task<Credentials> AddAsync(Credentials entity);
        Task<Credentials> DeleteByIdAsync(int id);
        Task<IEnumerable<Credentials>> GetAllAsync();
        Task<Credentials> GetByIdAsync(int id);
        Task<Credentials> GetByUserIdAsync(int userId);
        void Update(Credentials entity);
    }
}