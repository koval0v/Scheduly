using AutoMapper;
using Business.Interfaces;
using Business.Models;
using ScheduleService.Entities;
using SimpleService.Entities;
using SimpleService.Interfaces;

namespace Business.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        private readonly IScheduleDisciplineRepository _scheduleDisciplineRepository;

        private readonly IMapper _mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IScheduleDisciplineRepository scheduleDisciplineRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleDisciplineRepository = scheduleDisciplineRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleModel> AddAsync(ScheduleModel model)
        {
            var schedule = _mapper.Map<Schedule>(model);

            var scheduleCreated = await _scheduleRepository.AddAsync(schedule);

            await _scheduleRepository.SaveAsync();

            return _mapper.Map<ScheduleModel>(scheduleCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _scheduleRepository.DeleteByIdAsync(modelId);

            await _scheduleRepository.SaveAsync();
        }

        public async Task<IEnumerable<ScheduleModel>> GetAllAsync()
        {
            var schedules = await _scheduleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ScheduleModel>>(schedules);
        }

        public async Task<ScheduleModel> GetByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);

            return _mapper.Map<ScheduleModel>(schedule);
        }

        public async Task UpdateAsync(int id, ScheduleModel model)
        {
            var schedule = _mapper.Map<Schedule>(model);

            _scheduleRepository.Update(schedule);

            await _scheduleRepository.SaveAsync();
        }

        /* schedule disciplines */

        public async Task<ScheduleDisciplineModel> AddDisciplineAsync(ScheduleDisciplineModel model)
        {
            var scheduleDiscipline = _mapper.Map<ScheduleDiscipline>(model);

            var scheduleDisciplineCreated = await _scheduleDisciplineRepository.AddAsync(scheduleDiscipline);

            await _scheduleDisciplineRepository.SaveAsync();

            return _mapper.Map<ScheduleDisciplineModel>(scheduleDisciplineCreated);
        }

        public async Task DeleteDisciplineByIdAsync(int modelId)
        {
            await _scheduleDisciplineRepository.DeleteByIdAsync(modelId);

            await _scheduleDisciplineRepository.SaveAsync();
        }

        public async Task<IEnumerable<ScheduleDisciplineModel>> GetAllDisciplinesAsync()
        {
            var scheduleDisciplines = await _scheduleDisciplineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ScheduleDisciplineModel>>(scheduleDisciplines);
        }

        public async Task<ScheduleDisciplineModel> GetDisciplineByIdAsync(int id)
        {
            var scheduleDiscipline = await _scheduleDisciplineRepository.GetByIdAsync(id);

            return _mapper.Map<ScheduleDisciplineModel>(scheduleDiscipline);
        }

        public async Task UpdateDisciplineAsync(int id, ScheduleDisciplineModel model)
        {
            var scheduleDiscipline = _mapper.Map<ScheduleDiscipline>(model);

            _scheduleDisciplineRepository.Update(scheduleDiscipline);

            await _scheduleDisciplineRepository.SaveAsync();
        }
    }
}
