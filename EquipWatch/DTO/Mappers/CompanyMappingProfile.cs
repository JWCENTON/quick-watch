using AutoMapper;
using DTO.CompanyDTOs;

namespace DTO.Mappers;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Domain.Company.Models.Company, CreateCompanyDTO>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ReverseMap();

        CreateMap<Domain.Company.Models.Company, FullCompanyDTO>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ReverseMap();

        CreateMap<Domain.Company.Models.Company, UpdateCompanyDTO>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ReverseMap();

        CreateMap<Domain.Company.Models.Company, CompanyIdDTO>().ReverseMap();
    }
}