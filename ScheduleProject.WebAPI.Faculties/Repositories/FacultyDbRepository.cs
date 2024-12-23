using FacultyService.DbAccess;
using Microsoft.EntityFrameworkCore;
using ScheduleProject.WebAPI.Faculties.Entities;
using SimpleService.Entities;
using SimpleService.Interfaces;

namespace FacultyService.Repositories
{
    public class FacultyDbRepository : IFacultyRepository
    {
        private readonly FacultyDbContext _dbContext;

        public FacultyDbRepository(FacultyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Faculty> AddAsync(Faculty entity)
        {
            await _dbContext.Faculties.AddAsync(entity);

            return entity;
        }

        public async Task<Faculty> DeleteByIdAsync(int id)
        {
            var entity = await _dbContext.Faculties.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Faculty>> GetAllAsync()
        {
            return await _dbContext.Faculties.ToListAsync();
        }

        public async Task<Faculty> GetByIdAsync(int id)
        {
            return await _dbContext.Faculties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Faculty entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


        /* buildings */
        public async Task<Building> AddBuildingAsync(Building entity)
        {
            await _dbContext.Buildings.AddAsync(entity);

            return entity;
        }

        public async Task<Building> DeleteBuildingByIdAsync(int id)
        {
            var entity = await _dbContext.Buildings.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
        {
            return await _dbContext.Buildings.ToListAsync();
        }

        public async Task<Building> GetBuildingByIdAsync(int id)
        {
            return await _dbContext.Buildings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateBuilding(Building entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /* building rooms*/
        public async Task<BuildingRoom> AddBuildingRoomAsync(BuildingRoom entity)
        {
            await _dbContext.BuildingRooms.AddAsync(entity);

            return entity;
        }

        public async Task<BuildingRoom> DeleteBuildingRoomByIdAsync(int id)
        {
            var entity = await _dbContext.BuildingRooms.FindAsync(id);

            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<BuildingRoom>> GetAllBuildingRoomsAsync()
        {
            return await _dbContext.BuildingRooms.ToListAsync();
        }

        public async Task<BuildingRoom> GetBuildingRoomByIdAsync(int id)
        {
            return await _dbContext.BuildingRooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateBuildingRoom(BuildingRoom entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
