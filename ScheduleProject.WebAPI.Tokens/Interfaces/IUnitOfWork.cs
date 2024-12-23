namespace TokenService.Interfaces
{
    public interface IUnitOfWork
    {
        ICredentialsRepository CredentialsRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IEIRepository EIRepository { get; }

        Task SaveAsync();
    }
}