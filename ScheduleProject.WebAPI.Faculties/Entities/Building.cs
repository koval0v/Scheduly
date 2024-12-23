using SimpleService.Entities;

namespace ScheduleProject.WebAPI.Faculties.Entities
{
    public class Building : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int ShelterCapacity { get; set; }
        public int? UniversityId { get; set; }
    }
}
