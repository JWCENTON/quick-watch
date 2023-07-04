using AutoMapper;
using Domain.Employ.Models;
using DTO.EmployDTOs;

namespace DTO.Mappers;

public class EmployMappingProfile
{
    public EmployMappingProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
        {

            cfg.CreateMap<Employe, CreateEmployDTO>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Employe, FullEmployDTO>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Employe, UpdateEmployDTO>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        });

        var mapper = configuration.CreateMapper();
    }
}