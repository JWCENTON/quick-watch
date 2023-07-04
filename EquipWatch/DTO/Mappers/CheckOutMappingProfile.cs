using AutoMapper;
using DTO.CheckOutDTOs;

namespace DTO.Mappers;

public class CheckOutMappingProfile
{
    public CheckOutMappingProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
        {

            cfg.CreateMap<Domain.CheckOut.Models.CheckOut, CreateCheckOutDTO>()
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.CheckOut.Models.CheckOut, FullCheckOutDTO>()
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.CheckOut.Models.CheckOut, UpdateCheckOutDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        });

        var mapper = configuration.CreateMapper();
    }
}