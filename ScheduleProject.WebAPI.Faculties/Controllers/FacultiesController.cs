using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using System.Collections.Generic;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ScheduleProject.WebAPI.Faculties.Entities;
using ScheduleProject.WebAPI.Faculties.Models;
using SimpleService.Entities;

namespace SimpleService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FacultiesController : ControllerBase
    {
        private IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyModel>>> Get()
        {
            var faculties = await _facultyService.GetAllAsync();

            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyModel>> Get(int id)
        {
            var faculty = await _facultyService.GetByIdAsync(id);

            return Ok(faculty);
        }

        [HttpGet("byEI/{id}")]
        public async Task<ActionResult<IEnumerable<FacultyModel>>> GetByEI(int id)
        {
            var faculties = await _facultyService.GetAllAsync();

            return Ok(faculties.Where(p => p.UniversityId == id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _facultyService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FacultyModel model)
        {
            await _facultyService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add(FacultyModel model)
        {
            var created = await _facultyService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        /* buildings */

        [HttpGet("buildings")]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            var faculties = await _facultyService.GetAllBuildingsAsync();

            var rooms = await _facultyService.GetAllBuildingRoomsAsync();

            var models = faculties.Select(x => new BuildingModel()
            {
                Id = x.Id,
                Name = x.Name,
                ShelterCapacity = x.ShelterCapacity,
                UniversityId = x.UniversityId,
                BuildingRooms = rooms.Where(x => x.BuildingId == x.Id)
            });

            return Ok(faculties);
        }

        [HttpGet("buildings/{id}")]
        public async Task<ActionResult<BuildingModel>> GetBuilding(int id)
        {
            var faculty = await _facultyService.GetBuildingByIdAsync(id);

            var rooms = await _facultyService.GetAllBuildingRoomsAsync();

            var model = new BuildingModel()
            {
                Id = faculty.Id,
                Name = faculty.Name,
                ShelterCapacity = faculty.ShelterCapacity,
                UniversityId = faculty.UniversityId,
                BuildingRooms = rooms.Where(x => x.BuildingId == id)
            };

            return Ok(model);
        }

        [HttpGet("buildings/byEI/{id}")]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildingByEI(int id)
        {
            var faculties = await _facultyService.GetAllBuildingsAsync();

            var rooms = await _facultyService.GetAllBuildingRoomsAsync();

            var models = faculties.Select(x => new BuildingModel()
            {
                Id = x.Id,
                Name = x.Name,
                ShelterCapacity = x.ShelterCapacity,
                UniversityId = x.UniversityId,
                BuildingRooms = rooms.Where(m => m.BuildingId == x.Id)
            });

            return Ok(models.Where(p => p.UniversityId == id));
        }

        [HttpDelete("buildings/{id}")]
        public async Task<ActionResult> DeleteBuilding(int id)
        {
            await _facultyService.DeleteBuildingByIdAsync(id);

            return NoContent();
        }

        [HttpPut("buildings/{id}")]
        public async Task<ActionResult> UpdateBuilding(int id, Building model)
        {
            await _facultyService.UpdateBuildingAsync(id, model);

            return NoContent();
        }

        [HttpPost("buildings")]
        public async Task<ActionResult> AddBuilding(BuildingModel model)
        {
            var building = new Building()
            {
                ShelterCapacity = model.ShelterCapacity,
                Name = model.Name,
                UniversityId = model.UniversityId
            };
            var created = await _facultyService.AddBuildingAsync(building);

            foreach (var room in model.BuildingRooms)
            {
                var buildingRoom = new BuildingRoom()
                {
                    Capacity = room.Capacity,
                    Name = room.Name,
                    UniversityId = room.UniversityId,
                    BuildingId = created.Id
                };
                await _facultyService.AddBuildingRoomAsync(buildingRoom);
            }

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        /* building rooms */

        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<BuildingRoom>>> GetBuildingRooms()
        {
            var faculties = await _facultyService.GetAllBuildingRoomsAsync();

            return Ok(faculties.Where(x => x.BuildingId == 0));
        }

        [HttpGet("rooms/{id}")]
        public async Task<ActionResult<BuildingRoom>> GetRoom(int id)
        {
            var faculty = await _facultyService.GetBuildingRoomByIdAsync(id);

            return Ok(faculty);
        }

        [HttpGet("rooms/byEI/{id}")]
        public async Task<ActionResult<IEnumerable<BuildingRoom>>> GetBuildingRoomByEI(int id)
        {
            var faculties = await _facultyService.GetAllBuildingRoomsAsync();

            return Ok(faculties.Where(p => p.UniversityId == id));
        }

        [HttpDelete("rooms/{id}")]
        public async Task<ActionResult> DeleteBuildingRoom(int id)
        {
            await _facultyService.DeleteBuildingRoomByIdAsync(id);

            return NoContent();
        }

        [HttpPut("rooms/{id}")]
        public async Task<ActionResult> UpdateBuildingRoom(int id, BuildingRoom model)
        {
            await _facultyService.UpdateBuildingRoomAsync(id, model);

            return NoContent();
        }

        [HttpPost("rooms")]
        public async Task<ActionResult> AddBuildingRoom(BuildingRoom model)
        {
            var created = await _facultyService.AddBuildingRoomAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }
    }
}