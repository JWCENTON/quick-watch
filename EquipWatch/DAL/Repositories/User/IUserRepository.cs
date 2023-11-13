using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.User;

public interface IUserRepository
{
    Task<Domain.User.Models.User> FindByEmailAsync(string email);
    Task<bool> IsEmailConfirmedAsync(Domain.User.Models.User user);
    Task<SignInResult> PasswordSignInAsync(string email, string password);
}