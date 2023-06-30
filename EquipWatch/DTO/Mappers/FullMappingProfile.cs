using AutoMapper;
using Domain.Employ.Models;
using DTO.CheckInDTOs;
using DTO.CheckOutDTOs;
using DTO.ClientDTOs;
using DTO.CommissionDTOs;
using DTO.CompanyDTOs;
using DTO.EmployDTOs;
using DTO.EquipmentDTOs;
using DTO.InviteDTOs;
using DTO.UserDTOs;

namespace DTO.Mappers;

public class FullMappingProfile
{
    public FullMappingProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Equipment.Models.Equipment, FullEquipmentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();

            cfg.CreateMap<Domain.Company.Models.Company, FullCompanyDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ReverseMap();

            cfg.CreateMap<Domain.User.Models.User, FullUserDTO>().ReverseMap();

            cfg.CreateMap<Domain.Client.Models.Client, FullClientDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();

            cfg.CreateMap<Domain.Commission.Models.Commission.Commission, FullCommissionDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ReverseMap();

            cfg.CreateMap<Employe, FullEmployDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.CheckIn.Models.CheckIn, FullCheckInDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.CheckOut.Models.CheckOut, FullCheckOutDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.Invite.Models.Invite, FullInviteDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        })
        {

        };
        var mapper = configuration.CreateMapper();
    }
}