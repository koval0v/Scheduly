using ScheduleProject.WebAPI.Faculties.Entities;
using SimpleService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService.Interfaces
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        Task<IEnumerable<Building>> GetAllBuildingsAsync();
        Task<Building> GetBuildingByIdAsync(int id);
        Task<Building> AddBuildingAsync(Building entity);
        void UpdateBuilding(Building entity);
        Task<Building> DeleteBuildingByIdAsync(int id);

        Task<IEnumerable<BuildingRoom>> GetAllBuildingRoomsAsync();
        Task<BuildingRoom> GetBuildingRoomByIdAsync(int id);
        Task<BuildingRoom> AddBuildingRoomAsync(BuildingRoom entity);
        void UpdateBuildingRoom(BuildingRoom entity);
        Task<BuildingRoom> DeleteBuildingRoomByIdAsync(int id);
    }
}
