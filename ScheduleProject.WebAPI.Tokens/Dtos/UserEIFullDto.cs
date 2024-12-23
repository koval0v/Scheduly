using TokenService.Entities;

namespace TokenService.Models
{
    public class UserEIFullDto : BaseModel
    {
        public int UserId { get; set; }
        public int EIId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsAnswered { get; set; }
        public UserModel? User { get; set; }
    }
}
