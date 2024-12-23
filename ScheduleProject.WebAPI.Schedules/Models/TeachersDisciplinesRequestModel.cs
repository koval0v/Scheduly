using Business.Models;

namespace ScheduleService.Models
{
    public class DisciplinesRequestModel : BaseModel
    {
        public int GroupId { get; set; }
        public int Semester { get; set; }
    }
}
