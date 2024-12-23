using TokenService.Entities;

namespace TokenService.Models
{
    public class UserEIModel : BaseModel
    {
        public int UserId { get; set; }
        public int EIId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsAnswered { get; set; }
        public User? User { get; set; }
        public EI? EI { get; set; }
        public string? userEmailWhoSendInvite { get; set; }
    }
}
