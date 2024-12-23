using AutoMapper;
using DisciplineService.Dtos;
using DisciplineService.Entities;
using DisciplineService.Models;

namespace DisciplineService
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Group, GroupModel>()
                .ReverseMap();
        }
    }
}
