using Microsoft.EntityFrameworkCore;
using TokenService.DbAccess;
using TokenService.Entities;
using TokenService.Interfaces;

namespace TokenService.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly UserDbContext _userDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userDbContext">The forum database context.</param>
        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        /// <summary>
        /// Adds the User asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<User> AddAsync(User entity)
        {
            await _userDbContext.Users.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;User&gt;
        /// </returns>
        public async Task<User> DeleteByIdAsync(int id)
        {
            var entity = await _userDbContext.Users.FindAsync(id);

            if (entity != null)
            {
                _userDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;User&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userDbContext.Users
                .Include(u => u.Credentials)
                .ThenInclude(c => c.Role)
                .Include(p => p.UserEIs)
                .ThenInclude(m => m.EI)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the User by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Task&lt;User&gt;</returns>
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userDbContext.Users
                .Include(u => u.Credentials)
                .ThenInclude(c => c.Role)
                .Include(p => p.UserEIs)
                .ThenInclude(m => m.EI)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Gets the TEntity by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;User&gt;</returns>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _userDbContext.Users
                .Include(u => u.Credentials)
                .ThenInclude(c => c.Role)
                .Include(p => p.UserEIs)
                .ThenInclude(m => m.EI)
                .FirstOrDefaultAsync(u => u.Id == id); ;
        }

        /// <summary>
        /// Determines whether is email exist asynchronous
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// True, if email exists and false if not.
        /// </returns>
        public Task<bool> IsEmailExistAsync(string email)
        {
            return _userDbContext.Users.AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(User entity)
        {
            _userDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}