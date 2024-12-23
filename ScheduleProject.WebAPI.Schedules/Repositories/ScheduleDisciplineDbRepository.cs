using FacultyService.DbAccess;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Entities;
using SimpleService.Entities;
using SimpleService.Interfaces;

namespace FacultyService.Repositories
{
    public class ScheduleDisciplineDbRepository : IScheduleDisciplineRepository
    {
        private readonly ScheduleDbContext _dbContext;

        public ScheduleDisciplineDbRepository(ScheduleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ScheduleDiscipline> AddAsync(ScheduleDiscipline entity)
        {
            await _dbContext.ScheduleDisciplines.AddAsync(entity);

            return entity;
        }

        public async Task<ScheduleDiscipline> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.ScheduleDisciplines.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<ScheduleDiscipline>> GetAllAsync()
        {
            return await _dbContext.ScheduleDisciplines.ToListAsync();
        }

        public async Task<ScheduleDiscipline> GetByIdAsync(int id)
        {
            return await _dbContext.ScheduleDisciplines.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(ScheduleDiscipline entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
