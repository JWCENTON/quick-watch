using AutoMapper;
using Domain.Employee.Models;
using DTO.EmployDTOs;

namespace DTO.Mappers;

public class EmployMappingProfile : Profile
{
    public EmployMappingProfile()
    {
        CreateMap<Employee, CreateEmployDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();

        CreateMap<Employee, FullEmployDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();

        CreateMap<Employee, UpdateEmployDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();
    }
}