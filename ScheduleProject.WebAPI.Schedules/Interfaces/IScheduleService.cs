using Business.Models;

namespace Business.Interfaces
{
    public interface IScheduleService
    {
        Task<ScheduleModel> AddAsync(ScheduleModel model);
        Task<IEnumerable<ScheduleModel>> GetAllAsync();
        Task<ScheduleModel> GetByIdAsync(int id);
        Task UpdateAsync(int id, ScheduleModel model);
        Task DeleteByIdAsync(int modelId);
        Task<ScheduleDisciplineModel> AddDisciplineAsync(ScheduleDisciplineModel model);
        Task<IEnumerable<ScheduleDisciplineModel>> GetAllDisciplinesAsync();
        Task<ScheduleDisciplineModel> GetDisciplineByIdAsync(int id);
        Task UpdateDisciplineAsync(int id, ScheduleDisciplineModel model);
        Task DeleteDisciplineByIdAsync(int modelId);
    }
}
