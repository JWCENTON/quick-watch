using AutoMapper;
using DTO.CheckOutDTOs;

namespace DTO.Mappers;

public class CheckOutMappingProfile : Profile
{
    public CheckOutMappingProfile()
    {
        CreateMap<Domain.CheckOut.Models.CheckOut, CreateCheckOutDTO>().ReverseMap();

        CreateMap<Domain.CheckOut.Models.CheckOut, FullCheckOutDTO>().ReverseMap();

        CreateMap<Domain.CheckOut.Models.CheckOut, UpdateCheckOutDTO>();


        CreateMap<UpdateCheckOutDTO, Domain.CheckOut.Models.CheckOut>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));
    }
}