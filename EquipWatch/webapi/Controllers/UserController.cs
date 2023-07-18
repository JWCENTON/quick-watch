using Microsoft.AspNetCore.Identity;
using Domain.User.Models;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;
using NuGet.Common;
using webapi.Services;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
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
            _emailService.SendEmailForConfirmation(user, confirmationLink);
            // User registration successful
            // Return any necessary response or redirect
            return Ok();
        }
        else
        {
            // User registration failed
            // Return any necessary response or error messages
            return BadRequest(result.Errors);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                // User's email is not confirmed
                return Unauthorized(new { title = "EmailNotConfirmed" });
            }
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // User login successful
            // Return any necessary response or redirect
            return Ok("User has successfully logged in");
        }
        else
        {
            // User login failed
            // Return any necessary response or error messages
            return Unauthorized();
        }
    }

    [AllowAnonymous]
    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            // Invalid user ID or token
            return BadRequest("Invalid user ID or token");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            // User not found
            return NotFound("User not found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            // Email confirmed successfully
            return Redirect("https://localhost:3000/");
        }
        else
        {
            // Failed to confirm email
            return BadRequest("Failed to confirm email");
        }
    }

}
