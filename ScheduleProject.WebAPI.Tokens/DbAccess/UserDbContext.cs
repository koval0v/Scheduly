using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TokenService.Entities;

namespace TokenService.DbAccess
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<EI> EIs { get; set; }
        public DbSet<UserEI> UserEIs { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentApi
            var user = modelBuilder.Entity<User>();
            user.HasOne(u => u.Credentials)
                .WithOne(p => p.User)
                .HasForeignKey<Credentials>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            user.HasIndex(u => u.Email).IsUnique();
            user.Property(u => u.Email).HasMaxLength(100).IsRequired();
            user.ToTable("Users");

            var credentials = modelBuilder.Entity<Credentials>();
            credentials.HasOne(p => p.Role)
                .WithMany(r => r.Credentials)
                .OnDelete(DeleteBehavior.SetNull);
            credentials.Property(p => p.PasswordSalt).IsRequired();
            credentials.Property(p => p.PasswordHash).IsRequired();
            credentials.ToTable("Credentials");

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<EI>()
                .Property(r => r.Name).IsRequired();
            #endregion

            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, RoleName = "user" });
        }
    }
}