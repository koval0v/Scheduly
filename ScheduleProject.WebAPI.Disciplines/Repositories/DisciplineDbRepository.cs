using DisciplineService.DbAccess;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.Repositories
{
    public class DisciplineDbRepository : IDisciplineRepository
    {
        private readonly DisciplineDbContext _dbContext;

        public DisciplineDbRepository(DisciplineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discipline> AddAsync(Discipline entity)
        {
            await _dbContext.Disciplines.AddAsync(entity);

            return entity;
        }

        public async Task<Discipline> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Disciplines.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Discipline>> GetAllAsync()
        {
            return await _dbContext.Disciplines.ToListAsync();
        }

        public async Task<Discipline> GetByIdAsync(int id)
        {
            return await _dbContext.Disciplines.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Discipline entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
