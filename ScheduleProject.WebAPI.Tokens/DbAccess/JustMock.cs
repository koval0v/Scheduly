using Microsoft.EntityFrameworkCore;
using TokenService.Entities;

namespace TokenService.DbAccess
{
    public class JustMock
    {
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }

        public List<Credentials> Credentials { get; set; }
    }
}
