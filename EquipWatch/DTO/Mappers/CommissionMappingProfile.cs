using AutoMapper;
using DTO.CommissionDTOs;

namespace DTO.Mappers;

public class CommissionMappingProfile : Profile
{
    public CommissionMappingProfile()
    {
        CreateMap<Domain.Commission.Models.Commission.Commission, CreateCommissionDTO>().ReverseMap();

        CreateMap<Domain.Commission.Models.Commission.Commission, FullCommissionDTO>().ReverseMap();

        CreateMap<Domain.Commission.Models.Commission.Commission, UpdateCommissionDTO>();

        CreateMap<UpdateCommissionDTO, Domain.Commission.Models.Commission.Commission>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Domain.Commission.Models.Commission.Commission, CommissionIdDTO>().ReverseMap();
    }
}