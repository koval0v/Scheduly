namespace Data_access.Entities
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int? UniversityId { get; set; }

        public ICollection<DisciplineTeacher> DisciplineTeachers { get; set; }

    }
}
