using DisciplineService.Entities;
using DisciplineService.Enums;
using DisciplineService.Interfaces;

namespace DisciplineService.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly List<Discipline> _list = new() { new Discipline { Id = 1, Name = "Discipline1", Description = "Descipline1Description",
            Course = 1, CreditType = CreditType.Test, Hours = 80, IsSelective = true } };

        public async Task<Discipline> AddAsync(Discipline entity)
        {
            await Task.Run(() => _list.Add(entity));

            return entity;
        }

        public async Task<Discipline> DeleteByIdAsync(int id)
        {
            var entity = await Task.Run(() => _list.FirstOrDefault(x => x.Id == id));

            _list.Remove(entity);

            return entity;
        }

        public async Task<IEnumerable<Discipline>> GetAllAsync()
        {
            return await Task.Run(() => _list);
        }

        public async Task<Discipline> GetByIdAsync(int id)
        {
            var entity = await Task.Run(() => _list.FirstOrDefault(x => x.Id == id));

            return entity;
        }
        public async Task SaveAsync()
        {
            await Task.Delay(1);
        }
        public async void Update(Discipline entity)
        {
            var toUpdate = await Task.Run(() => _list.FirstOrDefault(x => x.Id == entity.Id));

            _list.Remove(toUpdate);
            _list.Add(entity);
        }
    }
}
