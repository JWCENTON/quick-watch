using AutoMapper;
using Domain.WorksOn.Models;
using DTO.UserDTOs;

namespace DTO.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Domain.User.Models.User, CreateUserDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, UserIdDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, FullUserDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, LoginUserDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, UpdateUserDTO>();


        CreateMap<UpdateUserDTO, Domain.User.Models.User>()
            .ForAllMembers(opt => opt
                .Condition((src, dest, srcMember) => srcMember != null));

    }
}