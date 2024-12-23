using DisciplineService.Enums;

namespace DisciplineService.Entities
{
    public class Discipline : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Course { get; set; }
        public CreditType CreditType { get; set; }
        public int Hours { get; set; }
        public bool IsSelective { get; set; }
        public int? CatalogId { get; set; }
        public int? UniversityId { get; set; }

        public ICollection<CatalogDiscipline> CatalogDisciplines { get; set; }
        public ICollection<SpecialtyDiscipline> SpecialtyDisciplines { get; set; }
    }
}
