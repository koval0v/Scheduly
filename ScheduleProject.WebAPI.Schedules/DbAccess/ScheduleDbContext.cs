using Microsoft.EntityFrameworkCore;
using ScheduleService.Entities;
using SimpleService.Entities;

namespace FacultyService.DbAccess
{
    public class ScheduleDbContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleDiscipline> ScheduleDisciplines { get; set; }

        public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
        {

        }

        public ScheduleDbContext()
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("string");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var schedule = modelBuilder.Entity<Schedule>();
            schedule.Property(x => x.GroupId).IsRequired();

            var scheduleDiscipline = modelBuilder.Entity<ScheduleDiscipline>();
            scheduleDiscipline.Property(x => x.ScheduleId).IsRequired();
            scheduleDiscipline.Property(x => x.DisciplineId).IsRequired();
            scheduleDiscipline.Property(x => x.TeacherId).IsRequired();
            scheduleDiscipline.Property(x => x.Semester).IsRequired();
            scheduleDiscipline.Property(x => x.Lesson).IsRequired();
            scheduleDiscipline.Property(x => x.Day).IsRequired();
            scheduleDiscipline.Property(x => x.IsLecture).IsRequired();
        }
    }
}
