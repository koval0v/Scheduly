using AutoMapper;

using DisciplineService.Entities;
using DisciplineService.Interfaces;
using DisciplineService.Models;

namespace CatalogService.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        private readonly IMapper _mapper;

        public CatalogService(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        public async Task<CatalogModel> AddAsync(CatalogModel model)
        {
            var catalog = _mapper.Map<Catalog>(model);

            var catalogCreated = await _catalogRepository.AddAsync(catalog);

            await _catalogRepository.SaveAsync();

            return _mapper.Map<CatalogModel>(catalogCreated);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _catalogRepository.DeleteByIdAsync(modelId);

            await _catalogRepository.SaveAsync();
        }

        public async Task<IEnumerable<CatalogModel>> GetAllAsync()
        {
            var catalogs = await _catalogRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CatalogModel>>(catalogs);
        }

        public async Task<CatalogModel> GetByIdAsync(int id)
        {
            var catalog = await _catalogRepository.GetByIdAsync(id);

            return _mapper.Map<CatalogModel>(catalog);
        }
    }
}
