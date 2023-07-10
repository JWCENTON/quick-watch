using AutoMapper;
using DTO.ClientDTOs;

namespace DTO.Mappers;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Domain.Client.Models.Client, CreateClientDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Client.Models.Client, ClientIdDTO>()
            .ReverseMap();

        CreateMap<Domain.Client.Models.Client, FullClientDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Client.Models.Client, UpdateClientDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();
    }
}