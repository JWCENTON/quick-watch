using AutoMapper;
using DTO.UserDTOs;

namespace DTO.Mappers;

public class UserMappingProfile
{
    public UserMappingProfile()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.User.Models.User, CreateUserDTO>().ReverseMap();

            cfg.CreateMap<Domain.User.Models.User, FullUserDTO>().ReverseMap();

            cfg.CreateMap<Domain.User.Models.User, UpdateUserDTO>().ReverseMap();

        });

        var mapper = configuration.CreateMapper();
    }
}