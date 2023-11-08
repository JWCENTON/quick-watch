using AutoMapper;
using Domain.WorksOn.Models;
using DTO.WorksOnDTOs;

namespace DTO.Mappers;

public class WorksOnMappingProfile : Profile
{
    public WorksOnMappingProfile()
    {
        CreateMap<WorksOn, CreateWorksOnDTO>().ReverseMap();

        CreateMap<WorksOn, FullWorksOnDTO>().ReverseMap();

        CreateMap<WorksOn, UpdateWorksOnDTO>();

        CreateMap<UpdateWorksOnDTO, WorksOn>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}