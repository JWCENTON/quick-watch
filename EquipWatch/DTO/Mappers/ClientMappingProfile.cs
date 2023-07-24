using AutoMapper;
using DTO.ClientDTOs;

namespace DTO.Mappers;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Domain.Client.Models.Client, CreateClientDTO>().ReverseMap();

        CreateMap<Domain.Client.Models.Client, PartialClientDTO>().ReverseMap();

        CreateMap<Domain.Client.Models.Client, FullClientDTO>().ReverseMap();

        CreateMap<Domain.Client.Models.Client, UpdateClientDTO>();

        CreateMap<UpdateClientDTO, Domain.Client.Models.Client>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}