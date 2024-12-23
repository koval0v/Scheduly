using DisciplineService.DbAccess;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.Repositories
{
    public class CatalogDbRepository : ICatalogRepository
    {
        private readonly DisciplineDbContext _dbContext;

        public CatalogDbRepository(DisciplineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Catalog> AddAsync(Catalog entity)
        {
            await _dbContext.Catalogs.AddAsync(entity);

            return entity;
        }

        public async Task<Catalog> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Catalogs.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Catalog>> GetAllAsync()
        {
            return await _dbContext.Catalogs.ToListAsync();
        }

        public async Task<Catalog> GetByIdAsync(int id)
        {
            return await _dbContext.Catalogs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Catalog entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
