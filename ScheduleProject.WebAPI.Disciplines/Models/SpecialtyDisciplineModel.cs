using DisciplineService.Enums;

namespace DisciplineService.Models
{
    public class SpecialtyDisciplineModel : BaseModel
    {
        public int SpecialtyId { get; set; }
        public int DisciplineId { get; set; }
        public int Semester { get; set; }
    }
}
