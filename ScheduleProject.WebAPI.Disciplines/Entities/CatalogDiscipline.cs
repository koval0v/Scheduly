using DisciplineService.Enums;

namespace DisciplineService.Entities
{
    public class CatalogDiscipline : BaseEntity
    {
        public int CatalogId { get; set; }
        public int DisciplineId { get; set; }
        public Catalog Catalog { get; set; }
        public Discipline Discipline { get; set; }
    }
}
