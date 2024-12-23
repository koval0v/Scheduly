using TokenService.Models;

namespace TokenService.Interfaces
{
    public interface IUserService
    {
        Task<string> GetTokenAsync(LoginRequest login);
        Task ChangeRoleAsync(int userId, int roleId);
        Task DeleteByIdAsync(int userId);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(int id);
        Task<int> GetIdByEmailAsync(string email);
        Task<IEnumerable<UserModel>> GetByRoleAsync(int roleId);
        Task<IEnumerable<RoleModel>> GetAllRolesAsync();
        Task<UserModel> RegisterAsync(RegistrationModel authModel);
        Task UpdateAsync(int id, UserModel userModel);
    }
}
