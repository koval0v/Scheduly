using DisciplineService.Enums;

namespace DisciplineService.Models
{
    public class CatalogModel : BaseModel
    {
        public string Name { get; set; } = String.Empty;
        public int? UniversityId { get; set; }
    }
}
