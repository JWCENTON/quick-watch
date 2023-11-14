using Domain.User.Models;
using Microsoft.AspNetCore.Identity;
using System.Web;

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

    public async Task<IdentityResult> ResetPasswordAsync(Domain.User.Models.User user, string token, string newPassword)
    {
        var result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), newPassword);
        return result;
    }

    public async Task<IdentityResult> UpdateAsync(Domain.User.Models.User user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result;
    }

    public async Task<IdentityResult> ChangePasswordAsync(Domain.User.Models.User user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result;
    }

    public async Task<IQueryable<Domain.User.Models.User>> GetAll()
    {
        var users = _userManager.Users;
        return users;
    }

    public async Task<List<Domain.User.Models.User>> GetAvailable(List<string> assignedUserIds)
    {
        var availableUsers = _userManager.Users
            .Where(user => !assignedUserIds.Contains(user.Id))
            .ToList();

        return availableUsers;
    }
}