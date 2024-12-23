using Business.Models;
using ScheduleProject.WebAPI.Faculties.Entities;

namespace Business.Interfaces
{
    public interface IFacultyService
    {
        Task<FacultyModel> AddAsync(FacultyModel model);
        Task<IEnumerable<FacultyModel>> GetAllAsync();
        Task<FacultyModel> GetByIdAsync(int id);
        Task UpdateAsync(int id, FacultyModel model);
        Task DeleteByIdAsync(int modelId);

        /* buildings */
        Task<Building> AddBuildingAsync(Building model);
        Task<IEnumerable<Building>> GetAllBuildingsAsync();
        Task<Building> GetBuildingByIdAsync(int id);
        Task UpdateBuildingAsync(int id, Building model);
        Task DeleteBuildingByIdAsync(int modelId);

        /* building rooms*/
        Task<BuildingRoom> AddBuildingRoomAsync(BuildingRoom model);
        Task<IEnumerable<BuildingRoom>> GetAllBuildingRoomsAsync();
        Task<BuildingRoom> GetBuildingRoomByIdAsync(int id);
        Task UpdateBuildingRoomAsync(int id, BuildingRoom model);
        Task DeleteBuildingRoomByIdAsync(int modelId);
    }
}
