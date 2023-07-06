using AutoMapper;
using DTO.UserDTOs;

namespace DTO.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Domain.User.Models.User, CreateUserDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, FullUserDTO>().ReverseMap();

        CreateMap<Domain.User.Models.User, UpdateUserDTO>().ReverseMap();
    }
}