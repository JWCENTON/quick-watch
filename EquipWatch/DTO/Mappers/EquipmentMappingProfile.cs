using AutoMapper;
using Domain.Equipment.Models;
using DTO.EquipmentDTOs;
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

        CreateMap<Domain.Equipment.Models.Equipment, EquipmentIdDTO>().ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, UpdateEquipmentLocationDTO>().ReverseMap();
    }
}