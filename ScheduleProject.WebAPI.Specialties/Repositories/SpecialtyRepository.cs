using Data_access.Entities;
using Data_access.Interfaces;

namespace Data_access.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private List<Specialty> _list = new List<Specialty>() { new Specialty { Id = 1, Name = "Specialty1" } };
        public async Task<Specialty> AddAsync(Specialty entity)
        {
            await Task.Run(() => _list.Add(entity));

            return entity;
        }

        public async Task<Specialty> DeleteByIdAsync(int id)
        {
            var entity = await Task.Run(() => _list.FirstOrDefault(x => x.Id == id));

            _list.Remove(entity);

            return entity;
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await Task.Run(() => _list);
        }

        public async Task<Specialty> GetByIdAsync(int id)
        {
            var entity = await Task.Run(() => _list.FirstOrDefault(x => x.Id == id));

            return entity;
        }

        public async Task SaveAsync()
        {
            await Task.Delay(1);
        }

        public async void Update(Specialty entity)
        {
            var toUpdate = await Task.Run(() => _list.FirstOrDefault(x => x.Id == entity.Id));

            _list.Remove(toUpdate);
            _list.Add(entity);
        }
    }
}
