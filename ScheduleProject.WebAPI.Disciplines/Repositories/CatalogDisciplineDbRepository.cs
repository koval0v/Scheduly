using DisciplineService.DbAccess;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.Repositories
{
    public class CatalogDisciplineDbRepository : ICatalogDisciplineRepository
    {
        private readonly DisciplineDbContext _dbContext;

        public CatalogDisciplineDbRepository(DisciplineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entities.CatalogDiscipline> AddAsync(Entities.CatalogDiscipline entity)
        {
            await _dbContext.CatalogDisciplines.AddAsync(entity);

            return entity;
        }

        public async Task<Entities.CatalogDiscipline> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.CatalogDisciplines.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Entities.CatalogDiscipline>> GetAllAsync()
        {
            return await _dbContext.CatalogDisciplines.Include(p => p.Discipline).ToListAsync();
        }

        public async Task<Entities.CatalogDiscipline> GetByIdAsync(int id)
        {
            return await _dbContext.CatalogDisciplines.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Entities.CatalogDiscipline entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
