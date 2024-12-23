using TokenService.Interfaces;

namespace TokenService.Mocks
{
    public class UnitOWMock : IUnitOfWork
    {
        public ICredentialsRepository CredentialsRepository => throw new NotImplementedException();

        public IRoleRepository RoleRepository => throw new NotImplementedException();

        public IUserRepository UserRepository => throw new NotImplementedException();
        public IEIRepository EIRepository => throw new NotImplementedException();

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
