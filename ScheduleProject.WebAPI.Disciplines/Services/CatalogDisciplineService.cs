using AutoMapper;

using DisciplineService.Entities;
using DisciplineService.Interfaces;
using DisciplineService.Models;

namespace CatalogDiscipline.Services
{
    public class CatalogDisciplineService : ICatalogDisciplineService
    {
        private readonly ICatalogDisciplineRepository _catalogDisciplineRepository;

        private readonly IMapper _mapper;

        public CatalogDisciplineService(ICatalogDisciplineRepository catalogDisciplineRepository, IMapper mapper)
        {
            _catalogDisciplineRepository = catalogDisciplineRepository;
            _mapper = mapper;
        }

        public async Task<CatalogDisciplineModel> AddAsync(CatalogDisciplineModel model)
        {
            var catalogDiscipline = _mapper.Map<DisciplineService.Entities.CatalogDiscipline>(model);

            var catalogDisciplineCreated = await _catalogDisciplineRepository.AddAsync(catalogDiscipline);

            await _catalogDisciplineRepository.SaveAsync();

            return _mapper.Map<CatalogDisciplineModel>(catalogDisciplineCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _catalogDisciplineRepository.DeleteByIdAsync(modelId);

            await _catalogDisciplineRepository.SaveAsync();
        }

        public async Task<IEnumerable<CatalogDisciplineModel>> GetAllAsync()
        {
            var catalogDisciplines = await _catalogDisciplineRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CatalogDisciplineModel>>(catalogDisciplines);
        }

        public async Task<IEnumerable<DisciplineModel>> GetDisciplinesByCatalogIdAsync(int catalogId)
        {
            var catalogDisciplines = await _catalogDisciplineRepository.GetAllAsync();

            return catalogDisciplines.Where(x => x.CatalogId == catalogId).Select(p => _mapper.Map<DisciplineModel>(p.Discipline));
        }

    }
}
