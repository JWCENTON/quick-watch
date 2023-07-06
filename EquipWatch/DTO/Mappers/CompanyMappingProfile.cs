using AutoMapper;
using DTO.CompanyDTOs;

namespace DTO.Mappers;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        //var configuration = new MapperConfiguration(cfg =>
        //{

        //    cfg.CreateMap<Domain.Company.Models.Company, CreateCompanyDTO>()
        //        .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
        //        .ReverseMap();

        //    cfg.CreateMap<Domain.Company.Models.Company, FullCompanyDTO>()
        //        .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
        //        .ReverseMap();

        //    cfg.CreateMap<Domain.Company.Models.Company, UpdateCompanyDTO>()
        //        .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
        //        .ReverseMap();
        //    cfg.CreateMap<Domain.Company.Models.Company, PartialCompanyDTO>()
        //        .ReverseMap();
        //});

        //var mapper = configuration.CreateMapper();
        CreateMap<Domain.Company.Models.Company, PartialCompanyDTO>().ReverseMap();
    }
}