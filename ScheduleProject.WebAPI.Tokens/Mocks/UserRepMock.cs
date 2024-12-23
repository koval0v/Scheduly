using TokenService.Entities;
using TokenService.Interfaces;

namespace TokenService.Mocks
{
    public class UserRepMock : IUserRepository
    {
        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailExistAsync(string email)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
