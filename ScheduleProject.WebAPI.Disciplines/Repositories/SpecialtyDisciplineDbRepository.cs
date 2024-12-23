using DisciplineService.DbAccess;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.Repositories
{
    public class SpecialtyDisciplineDbRepository : ISpecialtyDisciplineRepository
    {
        private readonly DisciplineDbContext _dbContext;

        public SpecialtyDisciplineDbRepository(DisciplineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SpecialtyDiscipline> AddAsync(SpecialtyDiscipline entity)
        {
            await _dbContext.SpecialtyDisciplines.AddAsync(entity);

            return entity;
        }

        public async Task<SpecialtyDiscipline> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.SpecialtyDisciplines.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<SpecialtyDiscipline>> GetAllAsync()
        {
            return await _dbContext.SpecialtyDisciplines.Include(p => p.Discipline).ToListAsync();
        }

        public async Task<SpecialtyDiscipline> GetByIdAsync(int id)
        {
            return await _dbContext.SpecialtyDisciplines.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(SpecialtyDiscipline entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
