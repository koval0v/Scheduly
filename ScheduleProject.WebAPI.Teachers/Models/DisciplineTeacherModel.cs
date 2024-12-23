namespace Business.Models
{
    public class DisciplineTeacherModel : BaseModel
    {
        public int DisciplineId { get; set; }
        public int TeacherId { get; set; }
        public bool isLecturer { get; set; }
    }
}
