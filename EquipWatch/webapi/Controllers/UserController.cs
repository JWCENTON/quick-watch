using Domain.User.Models;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.uow;
using webapi.Validators;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork,
        IEmailService emailService, IConfiguration configuration)


    {
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
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
