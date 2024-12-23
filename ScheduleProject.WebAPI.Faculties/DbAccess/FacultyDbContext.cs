using Microsoft.EntityFrameworkCore;
using ScheduleProject.WebAPI.Faculties.Entities;
using SimpleService.Entities;

namespace FacultyService.DbAccess
{
    public class FacultyDbContext : DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingRoom> BuildingRooms { get; set; }
        public FacultyDbContext(DbContextOptions<FacultyDbContext> options) : base(options)
        {

        }

        public FacultyDbContext()
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("string");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var faculty = modelBuilder.Entity<Faculty>();
            faculty.Property(x => x.Description).IsRequired().HasMaxLength(300);
            faculty.Property(x => x.Name).IsRequired().HasMaxLength(200);

            var building = modelBuilder.Entity<Building>();
            building.Property(x => x.Name).IsRequired().HasMaxLength(300);
            building.Property(x => x.ShelterCapacity).IsRequired();

            var buildingRoom = modelBuilder.Entity<BuildingRoom>();
            buildingRoom.Property(x => x.Name).IsRequired().HasMaxLength(300);
            buildingRoom.Property(x => x.Capacity).IsRequired();
        }
    }
}
