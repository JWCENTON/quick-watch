using System.Security.Claims;
using Domain.User.Models;
using DTO.UserDTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using webapi.uow;

namespace webapi.Services;
public class UserServices : IUserServices
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserServices(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public User MatchModelToNewUser(CreateUserDTO model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        return user;
    }

    public List<Claim> GenerateClaims(User user)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };
    }

    public void MatchModelToExistingUser(User user, UpdateUserCredentialsDTO model)
    {
        //user.FirstName = model.FirstName;
        //user.LastName = model.LastName;
        //user.Email = model.Email;
        _mapper.Map<UpdateUserCredentialsDTO>(user);
    }

    public string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var secretKey = _configuration[key: "JwtSettings:SecretKey"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "equip-watch",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetReactAppRedirectAddress()
    {
        return _configuration["ReactAppUrl"];
    }

    public string GetWebAppRedirectAddress()
    {
        return _configuration["WebApiUrl"];
    }

    public async Task<List<PartialUserDTO>> GetAllPartialUserDTOAsync()
    {
        var data = await _unitOfWork.User.GetAll();
        var allPartialUsersDTO = data.Select(user => _mapper.Map<PartialUserDTO>(user)).ToList();
        return allPartialUsersDTO;
    }

    public async Task<List<PartialUserDTO>> GetAvailableEmployeesAsync(Guid commissionId)
    {
        var worksOnList = await _unitOfWork.WorksOn.GeCurrentWorksOnByCommissionIdAsync(commissionId);

        var assignedUserIds = worksOnList.Select(work => work.UserId).ToList();

        var availableUsers = await _unitOfWork.User.GetAvailable(assignedUserIds);

        return availableUsers.Select(user => _mapper.Map<PartialUserDTO>(user)).ToList();
    }
}