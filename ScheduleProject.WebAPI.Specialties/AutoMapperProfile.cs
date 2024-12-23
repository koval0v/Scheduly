using AutoMapper;
using Business.Models;
using Data_access.Entities;
using SpecialtyService.Models;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Specialty, SpecialtyModel>()
            .ReverseMap();

        CreateMap<FacultySpecialty, FacultySpecialtyModel>()
            .ReverseMap();
    }
}