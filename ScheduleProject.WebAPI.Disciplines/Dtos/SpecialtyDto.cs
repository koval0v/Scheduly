using DisciplineService.Models;

namespace DisciplineService.Dtos
{
    public class SpecialtyDto : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Cipher { get; set; } = string.Empty;
        public int? UniversityId { get; set; }
    }
}
