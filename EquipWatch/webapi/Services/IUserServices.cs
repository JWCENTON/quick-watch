using Domain.User.Models;
using DTO.UserDTOs;
using System.Security.Claims;

namespace webapi.Services;
public interface IUserServices
{
    User MatchModelToNewUser(CreateUserDTO model);
    List<Claim> GenerateClaims(User user);
    string GenerateJwtToken(IEnumerable<Claim> claims);
    void MatchModelToExistingUser(User user, UpdateUserCredentialsDTO model);
    string GetReactAppRedirectAddress();
    string GetWebAppRedirectAddress();
    Task<List<PartialUserDTO>> GetAllPartialUserDTOAsync();
    Task<List<PartialUserDTO>> GetAvailableEmployeesAsync(Guid commissionId);
}