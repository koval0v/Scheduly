using FacultyService.DbAccess;
using Microsoft.EntityFrameworkCore;
using SimpleService.Entities;
using SimpleService.Interfaces;

namespace FacultyService.Repositories
{
    public class ScheduleDbRepository : IScheduleRepository
    {
        private readonly ScheduleDbContext _dbContext;

        public ScheduleDbRepository(ScheduleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Schedule> AddAsync(Schedule entity)
        {
            await _dbContext.Schedules.AddAsync(entity);

            return entity;
        }

        public async Task<Schedule> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Schedules.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _dbContext.Schedules.Include(p => p.ScheduleDisciplines).ToListAsync();
        }

        public async Task<Schedule> GetByIdAsync(int id)
        {
            return await _dbContext.Schedules.Include(p => p.ScheduleDisciplines).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Schedule entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
