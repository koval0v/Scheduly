namespace Business.Models
{
    public class TeacherModel : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int? UniversityId { get; set; }
    }
}
