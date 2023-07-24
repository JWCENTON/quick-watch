using AutoMapper;
using DTO.CheckInDTOs;

namespace DTO.Mappers;

public class CheckInMappingProfile : Profile
{
    public CheckInMappingProfile()
    {
        CreateMap<Domain.CheckIn.Models.CheckIn, CreateCheckInDTO>().ReverseMap();

        CreateMap<Domain.CheckIn.Models.CheckIn, FullCheckInDTO>().ReverseMap();

        CreateMap<Domain.CheckIn.Models.CheckIn, UpdateCheckInDTO>();

        CreateMap<UpdateCheckInDTO, Domain.CheckIn.Models.CheckIn>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}