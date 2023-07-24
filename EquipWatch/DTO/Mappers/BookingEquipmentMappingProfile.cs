using AutoMapper;
using Domain.BookedEquipment.Models;
using DTO.BookedEquipmentDTOs;

namespace DTO.Mappers;

public class BookingEquipmentMappingProfile : Profile
{
    public BookingEquipmentMappingProfile()
    {
        CreateMap<BookedEquipment, CreateBookedEquipmentDTO>().ReverseMap();

        CreateMap<BookedEquipment, FullBookedEquipmentDTO>().ReverseMap();

        CreateMap<BookedEquipment, UpdateBookedEquipmentDTO>();

        CreateMap<UpdateBookedEquipmentDTO, BookedEquipment>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}