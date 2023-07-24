using AutoMapper;
using Domain.Employee.Models;
using DTO.EmployDTOs;

namespace DTO.Mappers;

public class EmployMappingProfile : Profile
{
    public EmployMappingProfile()
    {
        CreateMap<Employee, CreateEmployDTO>().ReverseMap();

        CreateMap<Employee, FullEmployDTO>().ReverseMap();

        CreateMap<Employee, UpdateEmployDTO>();

        CreateMap<UpdateEmployDTO, Employee>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

    }
}