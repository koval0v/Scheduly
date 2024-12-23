using Data_access.Entities;
using Data_access.Interfaces;
using Microsoft.EntityFrameworkCore;
using TeacherService.DbAccess;

namespace TeacherService.Repositories
{
    public class TeacherDbRepository : ITeacherRepository
    {
        private readonly TeacherDbContext _dbContext;

        public TeacherDbRepository(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Teacher> AddAsync(Teacher entity)
        {
            await _dbContext.Teachers.AddAsync(entity);

            return entity;
        }

        public async Task<Teacher> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Teachers.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _dbContext.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Teacher entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
