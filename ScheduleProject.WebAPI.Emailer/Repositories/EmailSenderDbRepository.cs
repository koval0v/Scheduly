using FacultyService.DbAccess;
using Microsoft.EntityFrameworkCore;
using SimpleService.Entities;
using SimpleService.Interfaces;

namespace FacultyService.Repositories
{
    public class EmailSenderDbRepository : IEmailSenderRepository
    {
        private readonly EmailerDbContext _dbContext;

        public EmailSenderDbRepository(EmailerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmailSubscription> AddAsync(EmailSubscription entity)
        {
            await _dbContext.EmailSubscriptions.AddAsync(entity);

            return entity;
        }

        public async Task<EmailSubscription> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.EmailSubscriptions.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<EmailSubscription>> GetAllAsync()
        {
            return await _dbContext.EmailSubscriptions.ToListAsync();
        }

        public async Task<EmailSubscription> GetByIdAsync(int id)
        {
            return await _dbContext.EmailSubscriptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(EmailSubscription entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
