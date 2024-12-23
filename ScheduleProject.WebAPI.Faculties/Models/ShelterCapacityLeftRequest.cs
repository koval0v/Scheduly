namespace ScheduleProject.WebAPI.Faculties.Models
{
    public class ShelterCapacityLeftRequest
    {
        public int BuildingId { get; set; }
        public int Day { get; set; }
        public int Semester { get; set; }
        public int Lesson { get; set; }
    }
}
