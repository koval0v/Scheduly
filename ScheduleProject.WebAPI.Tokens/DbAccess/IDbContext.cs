using Microsoft.EntityFrameworkCore;
using TokenService.Entities;

namespace TokenService.DbAccess
{
    public interface IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Credentials> Credentials { get; set; }
    }
}
