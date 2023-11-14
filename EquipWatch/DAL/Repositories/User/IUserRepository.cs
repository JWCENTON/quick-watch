using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.User;

public interface IUserRepository
{
    Task<Domain.User.Models.User> FindByEmailAsync(string email);
    Task<bool> IsEmailConfirmedAsync(Domain.User.Models.User user);
    Task<SignInResult> PasswordSignInAsync(string email, string password);
    Task<IdentityResult> CreateAsync(Domain.User.Models.User user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(Domain.User.Models.User user);
    Task<Domain.User.Models.User?> FindByIdAsync(string userId);
    Task<IdentityResult> ConfirmEmailAsync(Domain.User.Models.User user, string token);
    Task<string> GeneratePasswordResetTokenAsync(Domain.User.Models.User user);
}