using TokenService.Entities;

namespace TokenService.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User entity);
        Task<User> DeleteByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
        Task<bool> IsEmailExistAsync(string email);
        void Update(User entity);
    }
}