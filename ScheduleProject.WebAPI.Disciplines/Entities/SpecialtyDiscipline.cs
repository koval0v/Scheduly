namespace DisciplineService.Entities
{
    public class SpecialtyDiscipline : BaseEntity
    {
        public int SpecialtyId { get; set; }
        public int DisciplineId { get; set; }
        public int Semester { get; set; }
        public Discipline Discipline { get; set; }
    }
}
