using Domain.User.Models;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Web;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
        IEmailService emailService, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDTO model)
    {
        
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(true);

        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "User", new { userId = user.Id, token }, Request.Scheme);

            await _emailService.SendEmailForConfirmationAsync(user, confirmationLink).ConfigureAwait(true);

            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return Unauthorized(new { title = "EmailNotConfirmed" });
            }
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var token = GenerateJwtToken(claims);
            return Ok(new { token });
        }
        else
        {
            return Unauthorized();
        }
   
    }

    [AllowAnonymous]
    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid user ID or token");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return Redirect("https://localhost:3000/");
        }
        else
        {
            return BadRequest("Failed to confirm email");
        }
    }

    [AllowAnonymous]
    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = Url.Action("ResetPassword", "User", new { userId = user.Id, token }, Request.Scheme);

        await _emailService.SendPasswordResetLinkAsync(user, resetLink);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("resetPassword")]
    public IActionResult RedirectResetPassword(string userId, string token)
    {
        var resetPasswordPageUrl = $"https://localhost:3000/resetPassword/{userId}/{HttpUtility.UrlEncode(token)}";

        return Redirect(resetPasswordPageUrl);
    }

    [AllowAnonymous]
    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(model.Token), model.NewPassword);

        if (result.Succeeded)
        {
            return Ok(new { message = "Password reset successful" });
        }
        else
        {
            return BadRequest(new { message = "Failed to reset password", errors = result.Errors });
        }
    }

    [Authorize]
    [HttpGet("getUserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var userInfo = new BaseUserDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        return Ok(userInfo);
    }

    [Authorize]
    [HttpPut("updateUserInfo")]
    public async Task<IActionResult> UpdateUserInfo([FromBody] BaseUserDTO model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok(new { message = "User information updated successfully" });
        }
        else
        {
            return BadRequest(new { message = "Failed to update user information", errors = result.Errors });
        }
    }

    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (result.Succeeded)
        {
            return Ok(new { message = "Password changed successfully" });
        }
        else
        {
            return BadRequest(new { message = "Failed to change password", errors = result.Errors });
        }
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims)
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
}