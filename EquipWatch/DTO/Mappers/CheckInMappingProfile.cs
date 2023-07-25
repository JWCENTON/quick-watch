using AutoMapper;
using DTO.CheckInDTOs;

namespace DTO.Mappers;

public class CheckInMappingProfile : Profile
{
    public CheckInMappingProfile()
    {
        CreateMap<Domain.CheckIn.Models.CheckIn, CreateCheckInDTO>()
            .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

        CreateMap<Domain.CheckIn.Models.CheckIn, FullCheckInDTO>()
            .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

        CreateMap<Domain.CheckIn.Models.CheckIn, UpdateCheckInDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();
    }
}