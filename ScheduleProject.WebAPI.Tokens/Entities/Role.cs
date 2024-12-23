namespace TokenService.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public ICollection<Credentials>? Credentials { get; set; }
    }
}
