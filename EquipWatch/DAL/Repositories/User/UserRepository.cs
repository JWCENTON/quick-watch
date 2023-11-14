using Domain.User.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly UserManager<Domain.User.Models.User> _userManager;
    private readonly SignInManager<Domain.User.Models.User> _signInManager;

    public UserRepository(UserManager<Domain.User.Models.User> userManager, SignInManager<Domain.User.Models.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Domain.User.Models.User> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null) return user;
        throw new InvalidOperationException(message:"User not found");
    }

    public async Task<bool> IsEmailConfirmedAsync(Domain.User.Models.User user)
    {
        var result = await _userManager.IsEmailConfirmedAsync(user);
        return result;
    }

    public async Task<SignInResult> PasswordSignInAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
        return result;
    }

    public async Task<IdentityResult> CreateAsync(Domain.User.Models.User user, string password)
    {
        var userCreationResult = await _userManager.CreateAsync(user, password);
        return userCreationResult;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(Domain.User.Models.User user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }

    public async Task<Domain.User.Models.User?> FindByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(Domain.User.Models.User user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(Domain.User.Models.User user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }
}