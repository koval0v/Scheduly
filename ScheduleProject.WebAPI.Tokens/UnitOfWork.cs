using TokenService.DbAccess;
using TokenService.Interfaces;
using TokenService.Repositories;

namespace TokenService
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly UserDbContext _userDbContext;
        private IUserRepository _userRepository;
        private ICredentialsRepository _credentialsRepository;
        private IRoleRepository _roleRepository;
        private IEIRepository _eiRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="userDbContext">The forum database context.</param>
        /// <param name="authDbContext">The authentication database context.</param>
        public UnitOfWork(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
            _userRepository = new UserRepository(userDbContext);
            _credentialsRepository = new CredentialsRepository(userDbContext);
            _roleRepository = new RoleRepository(userDbContext);
            _eiRepository = new EIRepository(userDbContext);
        }
        public IUserRepository UserRepository { get => _userRepository; }
        public ICredentialsRepository CredentialsRepository { get => _credentialsRepository; }
        public IRoleRepository RoleRepository { get => _roleRepository; }
        public IEIRepository EIRepository { get => _eiRepository; }

        /// <summary>
        /// Saves asynchronously.
        /// </summary>
        public async Task SaveAsync()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
