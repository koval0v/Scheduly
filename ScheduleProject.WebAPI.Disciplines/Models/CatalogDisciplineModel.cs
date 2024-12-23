using DisciplineService.Enums;

namespace DisciplineService.Models
{
    public class CatalogDisciplineModel : BaseModel
    {
        public int CatalogId { get; set; }
        public int DisciplineId { get; set; }
    }
}
