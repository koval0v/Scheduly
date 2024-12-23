using Data_access.Entities;
using Data_access.Interfaces;
using Microsoft.EntityFrameworkCore;
using TeacherService.DbAccess;

namespace TeacherService.Repositories
{
    public class DisciplineTeacherDbRepository : IDisciplineTeacherRepository
    {
        private readonly TeacherDbContext _dbContext;

        public DisciplineTeacherDbRepository(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DisciplineTeacher> AddAsync(DisciplineTeacher entity)
        {
            await _dbContext.DisciplineTeachers.AddAsync(entity);

            return entity;
        }

        public async Task<DisciplineTeacher> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.DisciplineTeachers.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<DisciplineTeacher>> GetAllAsync()
        {
            return await _dbContext.DisciplineTeachers.Include(x => x.Teacher).ToListAsync();
        }

        public async Task<DisciplineTeacher> GetByIdAsync(int id)
        {
            return await _dbContext.DisciplineTeachers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(DisciplineTeacher entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
