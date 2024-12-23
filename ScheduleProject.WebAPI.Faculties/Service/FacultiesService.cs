using AutoMapper;
using Business.Interfaces;
using Business.Models;
using ScheduleProject.WebAPI.Faculties.Entities;
using SimpleService.Entities;
using SimpleService.Interfaces;

namespace Business.Service
{
    public class FacultiesService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;

        private readonly IMapper _mapper;

        public FacultiesService(IFacultyRepository facultyRepository, IMapper mapper)
        {
            _facultyRepository = facultyRepository;
            _mapper = mapper;
        }

        public async Task<FacultyModel> AddAsync(FacultyModel model)
        {
            var faculty = _mapper.Map<Faculty>(model);

            var facultyCreated = await _facultyRepository.AddAsync(faculty);

            await _facultyRepository.SaveAsync();

            return _mapper.Map<FacultyModel>(facultyCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _facultyRepository.DeleteByIdAsync(modelId);

            await _facultyRepository.SaveAsync();
        }

        public async Task<IEnumerable<FacultyModel>> GetAllAsync()
        {
            var faculties = await _facultyRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<FacultyModel>>(faculties);
        }

        public async Task<FacultyModel> GetByIdAsync(int id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);

            return _mapper.Map<FacultyModel>(faculty);
        }

        public async Task UpdateAsync(int id, FacultyModel model)
        {
            var faculty = _mapper.Map<Faculty>(model);

            _facultyRepository.Update(faculty);

            await _facultyRepository.SaveAsync();
        }

        /* buildings */
        public async Task<Building> AddBuildingAsync(Building model)
        {
            var facultyCreated = await _facultyRepository.AddBuildingAsync(model);

            await _facultyRepository.SaveAsync();

            return facultyCreated;
        }

        public async Task DeleteBuildingByIdAsync(int modelId)
        {
            await _facultyRepository.DeleteBuildingByIdAsync(modelId);

            await _facultyRepository.SaveAsync();
        }

        public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
        {
            var faculties = await _facultyRepository.GetAllBuildingsAsync();

            return faculties;
        }

        public async Task<Building> GetBuildingByIdAsync(int id)
        {
            var faculty = await _facultyRepository.GetBuildingByIdAsync(id);

            return faculty;
        }

        public async Task UpdateBuildingAsync(int id, Building model)
        {
            _facultyRepository.UpdateBuilding(model);

            await _facultyRepository.SaveAsync();
        }

        /* building rooms */
        public async Task<BuildingRoom> AddBuildingRoomAsync(BuildingRoom model)
        {
            var facultyCreated = await _facultyRepository.AddBuildingRoomAsync(model);

            await _facultyRepository.SaveAsync();

            return facultyCreated;
        }

        public async Task DeleteBuildingRoomByIdAsync(int modelId)
        {
            await _facultyRepository.DeleteBuildingRoomByIdAsync(modelId);

            await _facultyRepository.SaveAsync();
        }

        public async Task<IEnumerable<BuildingRoom>> GetAllBuildingRoomsAsync()
        {
            var faculties = await _facultyRepository.GetAllBuildingRoomsAsync();

            return faculties;
        }

        public async Task<BuildingRoom> GetBuildingRoomByIdAsync(int id)
        {
            var faculty = await _facultyRepository.GetBuildingRoomByIdAsync(id);

            return faculty;
        }

        public async Task UpdateBuildingRoomAsync(int id, BuildingRoom model)
        {
            _facultyRepository.UpdateBuildingRoom(model);

            await _facultyRepository.SaveAsync();
        }
    }
}
