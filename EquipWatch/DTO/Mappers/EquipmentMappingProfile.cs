using AutoMapper;
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

public class EquipmentMappingProfile : Profile
{
    public EquipmentMappingProfile()
    {
        CreateMap<Domain.Equipment.Models.Equipment, CreateEquipmentDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, FullEquipmentDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, UpdateEquipmentDTO>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            .ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, PartialEquipmentDTO>().ReverseMap();
    }
}