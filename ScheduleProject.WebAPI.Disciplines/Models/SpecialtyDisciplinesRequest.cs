namespace DisciplineService.Models
{
    public class SpecialtyDisciplinesRequestModel : BaseModel
    {
        public int SpecialtyId { get; set; }
        public int Semester { get; set; }
    }
}
