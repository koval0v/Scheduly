using TokenService.Entities;

namespace TokenService.Models
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}