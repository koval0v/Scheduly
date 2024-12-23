using AutoMapper;
using Microsoft.Extensions.Hosting;
using TokenService.Dtos;
using TokenService.Entities;
using TokenService.Models;

namespace TokenService
{
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomapperProfile"/> class.
        /// </summary>
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(um => um.RoleName, x => x.MapFrom(u => u.Credentials.Role.RoleName))
                .ForMember(um => um.EIs, x => x.MapFrom(u => u.UserEIs.Select(m => m.EI)))
                .ReverseMap();

            CreateMap<Role, RoleModel>().ReverseMap();

            CreateMap<EI, EIModel>().ReverseMap();

            CreateMap<UserEI, UserEIModel>().ReverseMap();
        }
    }
}
