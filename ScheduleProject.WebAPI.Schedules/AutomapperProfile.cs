using AutoMapper;
using Business.Models;
using ScheduleService.Entities;
using SimpleService.Entities;

namespace Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Schedule, ScheduleModel>()
                .ReverseMap();

            CreateMap<ScheduleDiscipline, ScheduleDisciplineModel>()
               .ReverseMap();
        }
    }
}
