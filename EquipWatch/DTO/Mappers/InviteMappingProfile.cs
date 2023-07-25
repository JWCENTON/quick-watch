using AutoMapper;
using DTO.InviteDTOs;

namespace DTO.Mappers;

public class InviteMappingProfile : Profile
{
    public InviteMappingProfile()
    {
        CreateMap<Domain.Invite.Models.Invite, CreateInviteDTO>().ReverseMap();

        CreateMap<Domain.Invite.Models.Invite, FullInviteDTO>().ReverseMap();

        CreateMap<Domain.Invite.Models.Invite, UpdateInviteDTO>();

        CreateMap<UpdateInviteDTO, Domain.Invite.Models.Invite>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}