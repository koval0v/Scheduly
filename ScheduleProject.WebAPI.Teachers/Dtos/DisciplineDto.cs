using Business.Models;
using DisciplineService.Enums;

namespace TeacherService.Dtos
{
    public class DisciplineDto : BaseModel
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Course { get; set; }
        public CreditType CreditType { get; set; }
        public int Hours { get; set; }
        public bool IsSelective { get; set; }
        public int? CatalogId { get; set; }
    }
}
