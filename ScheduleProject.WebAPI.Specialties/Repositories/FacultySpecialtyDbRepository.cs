using Data_access.Entities;
using Data_access.Interfaces;
using Microsoft.EntityFrameworkCore;
using SpecialtyService.DbAccess;

namespace SpecialtyService.Repositories
{
    public class FacultySpecialtyDbRepository : IFacultySpecialtyRepository
    {
        private readonly SpecialtyDbContext _dbContext;

        public FacultySpecialtyDbRepository(SpecialtyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FacultySpecialty> AddAsync(FacultySpecialty entity)
        {
            await _dbContext.FacultySpecialties.AddAsync(entity);

            return entity;
        }

        public async Task<FacultySpecialty> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.FacultySpecialties.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<FacultySpecialty>> GetAllAsync()
        {
            return await _dbContext.FacultySpecialties.Include(p => p.Specialty).ToListAsync();
        }

        public async Task<FacultySpecialty> GetByIdAsync(int id)
        {
            return await _dbContext.FacultySpecialties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(FacultySpecialty entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
