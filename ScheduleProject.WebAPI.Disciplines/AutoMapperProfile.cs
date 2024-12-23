using AutoMapper;
using DisciplineService.Entities;
using DisciplineService.Models;

namespace DisciplineService
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Discipline, DisciplineModel>()
                .ReverseMap();

            CreateMap<Catalog, CatalogModel>()
                .ReverseMap();

            CreateMap<Entities.CatalogDiscipline, CatalogDisciplineModel>()
                .ReverseMap();

            CreateMap<SpecialtyDiscipline, SpecialtyDisciplineModel>()
                .ReverseMap();
        }
    }
}
