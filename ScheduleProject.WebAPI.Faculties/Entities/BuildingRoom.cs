using SimpleService.Entities;

namespace ScheduleProject.WebAPI.Faculties.Entities
{
    public class BuildingRoom : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int BuildingId { get; set; }
        public int? UniversityId { get; set; }
    }
}
