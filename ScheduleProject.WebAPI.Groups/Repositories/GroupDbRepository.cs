using DisciplineService.DbAccess;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.Repositories
{
    public class GroupDbRepository : IGroupRepository
    {
        private readonly GroupDbContext _dbContext;

        public GroupDbRepository(GroupDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Group> AddAsync(Group entity)
        {
            await _dbContext.Groups.AddAsync(entity);

            return entity;
        }

        public async Task<Group> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Groups.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _dbContext.Groups.ToListAsync();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Group entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
