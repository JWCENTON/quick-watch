using Domain.User.Models;
using DTO.UserDTOs;
using System.Security.Claims;

namespace webapi.Services;
public interface IUserServices
{
    public User MatchModelToNewUser(CreateUserDTO model);
    public List<Claim> GenerateClaims(User user);
    public string GenerateJwtToken(IEnumerable<Claim> claims);
    public void MatchModelToExistingUser(User user, UpdateUserCredentialsDTO model);
    public string GetReactAppRedirectAddress();
    public string GetWebAppRedirectAddress();
}