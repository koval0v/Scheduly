using Business.Models;
using ScheduleProject.WebAPI.Faculties.Entities;

namespace ScheduleProject.WebAPI.Faculties.Models
{
    public class BuildingModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public int ShelterCapacity { get; set; }
        public int? UniversityId { get; set; }
        public IEnumerable<BuildingRoom> BuildingRooms { get; set; }
    }
}
