using AutoMapper;
using Business.Models;
using Data_access.Entities;

namespace TeacherService
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Teacher, TeacherModel>()
                .ReverseMap();

            CreateMap<DisciplineTeacher, DisciplineTeacherModel>()
                .ReverseMap();
        }
    }
}