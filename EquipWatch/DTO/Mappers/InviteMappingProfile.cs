using AutoMapper;
using DTO.InviteDTOs;

namespace DTO.Mappers;

public class InviteMappingProfile : Profile
{
    public InviteMappingProfile()
    {
        CreateMap<Domain.Invite.Models.Invite, CreateInviteDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();

        CreateMap<Domain.Invite.Models.Invite, FullInviteDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();

        CreateMap<Domain.Invite.Models.Invite, UpdateInviteDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();
    }
}