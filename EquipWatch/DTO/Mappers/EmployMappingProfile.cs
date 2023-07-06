using AutoMapper;
using Domain.Employ.Models;
using DTO.EmployDTOs;

namespace DTO.Mappers;

public class EmployMappingProfile : Profile
{
    public EmployMappingProfile()
    {
        CreateMap<Employe, CreateEmployDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

        CreateMap<Employe, FullEmployDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

        CreateMap<Employe, UpdateEmployDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();
    }
}