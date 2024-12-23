using Data_access.Entities;
using Data_access.Interfaces;
using Microsoft.EntityFrameworkCore;
using SpecialtyService.DbAccess;

namespace SpecialtyService.Repositories
{
    public class SpecialtyDbRepository : ISpecialtyRepository
    {
        private readonly SpecialtyDbContext _dbContext;

        public SpecialtyDbRepository(SpecialtyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Specialty> AddAsync(Specialty entity)
        {
            await _dbContext.Specialties.AddAsync(entity);

            return entity;
        }

        public async Task<Specialty> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Specialties.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await _dbContext.Specialties.ToListAsync();
        }

        public async Task<Specialty> GetByIdAsync(int id)
        {
            return await _dbContext.Specialties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Specialty entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
