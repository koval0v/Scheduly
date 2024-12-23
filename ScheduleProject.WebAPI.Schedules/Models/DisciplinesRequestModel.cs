using Business.Models;

namespace ScheduleService.Models
{
    public class TeacherDisciplinesRequestModel : BaseModel
    {
        public int TeacherId { get; set; }
        public int Semester { get; set; }
    }
}
