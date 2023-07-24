using AutoMapper;
using DTO.EquipmentDTOs;
namespace DTO.Mappers;

public class EquipmentMappingProfile : Profile
{
    public EquipmentMappingProfile()
    {
        CreateMap<Domain.Equipment.Models.Equipment, CreateEquipmentDTO>().ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, FullEquipmentDTO>().ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, UpdateEquipmentDTO>();

        CreateMap<UpdateEquipmentDTO, Domain.Equipment.Models.Equipment>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Domain.Equipment.Models.Equipment, PartialEquipmentDTO>().ReverseMap();

        CreateMap<Domain.Equipment.Models.Equipment, EquipmentIdDTO>().ReverseMap();
    }
}