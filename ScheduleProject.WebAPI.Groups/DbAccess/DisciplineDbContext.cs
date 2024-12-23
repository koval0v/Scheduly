using DisciplineService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.DbAccess
{
    public class GroupDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }


        public GroupDbContext(DbContextOptions<GroupDbContext> options) : base(options)
        {

        }

        public GroupDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var group = modelBuilder.Entity<Group>();
            group.Property(x => x.Cipher).IsRequired().HasMaxLength(10);
            group.Property(x => x.Course).IsRequired();
            group.Property(x => x.FacultyId).IsRequired();
            group.Property(x => x.SpecialtyId).IsRequired();
            group.Property(x => x.StudentsQuantity).IsRequired();
        }
    }
}
