using AutoMapper;
using DTO.ClientDTOs;

namespace DTO.Mappers;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        //var configuration = new MapperConfiguration(cfg =>
        //{

        //    cfg.CreateMap<Domain.Client.Models.Client, CreateClientDTO>()
        //        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
        //        .ReverseMap();

        //    cfg.CreateMap<Domain.Client.Models.Client, FullClientDTO>()
        //        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
        //        .ReverseMap();

        //    cfg.CreateMap<Domain.Client.Models.Client, UpdateClientDTO>()
        //        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
        //        .ReverseMap();
        //});

        //var mapper = configuration.CreateMapper();

        CreateMap<Domain.Client.Models.Client, CreateClientDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Client.Models.Client, FullClientDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Client.Models.Client, UpdateClientDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();
    }
}