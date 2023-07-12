using Microsoft.AspNetCore.Identity;
using Domain.User.Models;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
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
            // User registration successful
            // Return any necessary response or redirect
            await _unitOfWork.Users.CreateAsync(user).ConfigureAwait(true);
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
    public async Task<IActionResult> Login([FromBody] CreateUserDTO model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // User login successful
            // Return any necessary response or redirect
            return Ok();
        }
        else
        {
            // User login failed
            // Return any necessary response or error messages
            return Unauthorized();
        }
    }
}