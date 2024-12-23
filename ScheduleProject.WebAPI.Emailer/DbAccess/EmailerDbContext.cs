using Microsoft.EntityFrameworkCore;
using SimpleService.Entities;

namespace FacultyService.DbAccess
{
    public class EmailerDbContext : DbContext
    {
        public DbSet<EmailSubscription> EmailSubscriptions { get; set; }

        public EmailerDbContext(DbContextOptions<EmailerDbContext> options) : base(options)
        {

        }

        public EmailerDbContext()
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("string");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var emailSubscriptipn = modelBuilder.Entity<EmailSubscription>();
            emailSubscriptipn.Property(x => x.Email).IsRequired();
            emailSubscriptipn.Property(x => x.ScheduleId).IsRequired();
        }
    }
}
