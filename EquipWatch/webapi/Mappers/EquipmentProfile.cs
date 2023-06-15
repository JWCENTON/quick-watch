using AutoMapper;
using Domain;
using webapi.Models;

namespace webapi.Mappers
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<Equipment, EquipmentDto>().ReverseMap();
            CreateMap<Equipment, CreateEquipmentDto>().ReverseMap();
            CreateMap<Equipment, UpdateEquipmentDto>().ReverseMap();
        }
    }
}
