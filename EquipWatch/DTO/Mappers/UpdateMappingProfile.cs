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

public class UpdateMappingProfile
{
    public UpdateMappingProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Equipment.Models.Equipment, UpdateEquipmentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();

            cfg.CreateMap<Domain.Company.Models.Company, UpdateCompanyDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ReverseMap();

            cfg.CreateMap<Domain.User.Models.User, UpdateUserDTO>().ReverseMap();

            cfg.CreateMap<Domain.Client.Models.Client, UpdateClientDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();

            cfg.CreateMap<Domain.Commission.Models.Commission.Commission, UpdateCommissionDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ReverseMap();

            cfg.CreateMap<Employe, UpdateEmployDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.CheckIn.Models.CheckIn, UpdateCheckInDTO>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.CheckOut.Models.CheckOut, UpdateCheckOutDTO>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            cfg.CreateMap<Domain.Invite.Models.Invite, UpdateInviteDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        });

        var mapper = configuration.CreateMapper();
    }
    
}