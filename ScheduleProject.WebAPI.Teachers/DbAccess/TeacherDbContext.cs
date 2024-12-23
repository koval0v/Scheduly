using Data_access.Entities;
using Microsoft.EntityFrameworkCore;

namespace TeacherService.DbAccess
{
    public class TeacherDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<DisciplineTeacher> DisciplineTeachers { get; set; }

        public TeacherDbContext(DbContextOptions<TeacherDbContext> options) : base(options)
        {

        }

        public TeacherDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var teacher = modelBuilder.Entity<Teacher>();
            teacher.Property(x => x.Name).IsRequired().HasMaxLength(50);
            teacher.Property(x => x.Surname).IsRequired().HasMaxLength(50);
            teacher.Property(x => x.Patronymic).IsRequired().HasMaxLength(50);

            var disciplineTeacher = modelBuilder.Entity<DisciplineTeacher>();
            disciplineTeacher.Property(x => x.TeacherId).IsRequired();
            disciplineTeacher.Property(x => x.DisciplineId).IsRequired();
            disciplineTeacher.Property(x => x.isLecturer).IsRequired();
        }

    }
}
