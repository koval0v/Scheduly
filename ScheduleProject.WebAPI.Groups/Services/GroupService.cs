using AutoMapper;
using DisciplineService.Dtos;
using DisciplineService.Entities;
using DisciplineService.Interfaces;
using DisciplineService.Models;

namespace CatalogService.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupModel> AddAsync(GroupModel model)
        {
            var group = _mapper.Map<Group>(model);

            var groupCreated = await _groupRepository.AddAsync(group);

            await _groupRepository.SaveAsync();

            return _mapper.Map<GroupModel>(groupCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _groupRepository.DeleteByIdAsync(modelId);

            await _groupRepository.SaveAsync();
        }

        public async Task<IEnumerable<GroupModel>> GetAllAsync()
        {
            var groups = await _groupRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<GroupModel>>(groups);
        }

        public async Task<GroupModel> GetByIdAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            return _mapper.Map<GroupModel>(group);
        }

        public async Task UpdateAsync(int id, GroupModel model)
        {
            var group = _mapper.Map<Group>(model);

            await Task.Run(() => _groupRepository.Update(group));

            await _groupRepository.SaveAsync();
        }
    }
}
