using Microsoft.EntityFrameworkCore;
using TokenService.DbAccess;
using TokenService.Entities;
using TokenService.Interfaces;

namespace TokenService.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The authentication database context
        /// </summary>
        private readonly UserDbContext _userDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="authDbContext">The authentication database context.</param>
        public RoleRepository(UserDbContext authDbContext)
        {
            _userDbContext = authDbContext;
        }

        /// <summary>
        /// Adds the Role asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<Role> AddAsync(Role entity)
        {
            await _userDbContext.Roles.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the Role by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Role&gt;</returns>
        public async Task<Role> DeleteByIdAsync(int id)
        {
            var entity = await _userDbContext.Roles.FindAsync(id);

            if (entity != null)
            {
                _userDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Role&gt;&gt;.</returns>
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _userDbContext.Roles
                .Include(r => r.Credentials)
                .ThenInclude(c => c.User)
                .ToListAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Role entity)
        {
            _userDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Role&gt;</returns>
        public async Task<Role> GetByIdAsync(int id)
        {
            return await _userDbContext.Roles
                .Include(r => r.Credentials)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
