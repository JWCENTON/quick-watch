using AutoMapper;
using DTO.InviteDTOs;

namespace DTO.Mappers;

public class InviteMappingProfile : Profile
{
    public InviteMappingProfile()
    {
        //var configuration = new MapperConfiguration(cfg =>
        //{

        //    cfg.CreateMap<Domain.Invite.Models.Invite, CreateInviteDTO>()
        //        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
        //        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
        //        .ReverseMap();

        //    cfg.CreateMap<Domain.Invite.Models.Invite, FullInviteDTO>()
        //        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
        //        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
        //        .ReverseMap();

        //    cfg.CreateMap<Domain.Invite.Models.Invite, UpdateInviteDTO>()
        //        .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
        //        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
        //        .ReverseMap();
        //});

        //var mapper = configuration.CreateMapper();

        CreateMap<Domain.Invite.Models.Invite, CreateInviteDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

        CreateMap<Domain.Invite.Models.Invite, FullInviteDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

        CreateMap<Domain.Invite.Models.Invite, UpdateInviteDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();
    }
}