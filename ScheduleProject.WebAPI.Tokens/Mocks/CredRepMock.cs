using TokenService.Entities;
using TokenService.Interfaces;

namespace TokenService.Mocks
{
    public class CredRepMock : ICredentialsRepository
    {
        public Task<Credentials> AddAsync(Credentials entity)
        {
            throw new NotImplementedException();
        }

        public Task<Credentials> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Credentials>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Credentials> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Credentials> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Credentials entity)
        {
            throw new NotImplementedException();
        }
    }
}
