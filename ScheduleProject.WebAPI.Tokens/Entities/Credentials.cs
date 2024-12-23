using System.Data;

namespace TokenService.Entities
{
    public class Credentials : BaseEntity
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public User User { get; set; }
        public Role? Role { get; set; }
    }
}
