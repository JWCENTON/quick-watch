using AutoMapper;
using DTO.CompanyDTOs;

namespace DTO.Mappers;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Domain.Company.Models.Company, CreateCompanyDTO>().ReverseMap();

        CreateMap<Domain.Company.Models.Company, FullCompanyDTO>().ReverseMap();

        CreateMap<Domain.Company.Models.Company, UpdateCompanyDTO>();

        CreateMap<UpdateCompanyDTO, Domain.Company.Models.Company>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Domain.Company.Models.Company, CompanyIdDTO>().ReverseMap();
    }
}