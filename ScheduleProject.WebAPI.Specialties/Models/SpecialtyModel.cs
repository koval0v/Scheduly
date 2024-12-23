namespace Business.Models
{
    public class SpecialtyModel : BaseModel
    {
        public string Cipher { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UniversityId { get; set; }
    }
}
