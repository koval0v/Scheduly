using TokenService.Entities;
using TokenService.Interfaces;

namespace TokenService.Mocks
{
    public class RoleRepMock : IRoleRepository
    {
        public Task<Role> AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<Role> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
