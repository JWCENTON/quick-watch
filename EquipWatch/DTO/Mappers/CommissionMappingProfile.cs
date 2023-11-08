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

        CreateMap<Domain.Commission.Models.Commission.Commission, PartialCommissionDTO>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString("dd-MM-yyyy HH:mm")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime != null ? src.EndTime.Value.ToString("dd-MM-yyyy HH:mm") : null));

        CreateMap<PartialCommissionDTO, Domain.Commission.Models.Commission.Commission>();
    }
}