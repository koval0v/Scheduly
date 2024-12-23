using DisciplineService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DisciplineService.DbAccess
{
    public class DisciplineDbContext : DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Entities.CatalogDiscipline> CatalogDisciplines { get; set; }
        public DbSet<Entities.SpecialtyDiscipline> SpecialtyDisciplines { get; set; }


        public DisciplineDbContext(DbContextOptions<DisciplineDbContext> options) : base(options)
        {

        }

        public DisciplineDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var discipline = modelBuilder.Entity<Discipline>();
            discipline.Property(x => x.Name).IsRequired().HasMaxLength(50);
            discipline.Property(x => x.Course).IsRequired();
            discipline.Property(x => x.Description).IsRequired().HasMaxLength(200);
            discipline.Property(x => x.CreditType).IsRequired();
            discipline.Property(x => x.IsSelective).IsRequired();
            discipline.Property(x => x.Hours).IsRequired();

            var catalog = modelBuilder.Entity<Catalog>();
            catalog.Property(x => x.Name).IsRequired().HasMaxLength(10);

            var catalogDiscipline = modelBuilder.Entity<Entities.CatalogDiscipline>();
            catalogDiscipline.Property(x => x.CatalogId).IsRequired();
            catalogDiscipline.Property(x => x.DisciplineId).IsRequired();

            var specialtyDiscipline = modelBuilder.Entity<SpecialtyDiscipline>();
            specialtyDiscipline.Property(x => x.SpecialtyId).IsRequired();
            specialtyDiscipline.Property(x => x.DisciplineId).IsRequired();
            specialtyDiscipline.Property(x => x.Semester).IsRequired();
        }
    }
}
