using Microsoft.EntityFrameworkCore;
using TokenService.DbAccess;
using TokenService.Entities;
using TokenService.Interfaces;

namespace TokenService.Repositories
{
    public class EIRepository : IEIRepository
    {
        /// <summary>
        /// The authentication database context
        /// </summary>
        private readonly UserDbContext _userDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EIRepository"/> class.
        /// </summary>
        /// <param name="authDbContext">The authentication database context.</param>
        public EIRepository(UserDbContext authDbContext)
        {
            _userDbContext = authDbContext;
        }

        public async Task<IEnumerable<EI>> GetAllAsync()
        {
            return await _userDbContext.EIs.ToListAsync();
        }

        /// <summary>
        /// Adds the Role asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<EI> AddAsync(EI entity)
        {
            await _userDbContext.EIs.AddAsync(entity);

            return entity;
        }

        public async Task<UserEI> AddUserEIAsync(UserEI entity)
        {
            await _userDbContext.UserEIs.AddAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<UserEI>> GetAllUserEIAsync()
        {
            return await _userDbContext.UserEIs.ToListAsync();
        }

        public void UpdateUserEI(UserEI entity)
        {
            _userDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<UserEI> DeleteUserEIByIdAsync(int id)
        {
            var entity = await _userDbContext.UserEIs.FindAsync(id);

            if (entity != null)
            {
                _userDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }
    }
}
