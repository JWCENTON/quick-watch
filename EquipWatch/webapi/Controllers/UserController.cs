using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using System.Security.Claims;
using System.Web;
using Domain.User.Models;
using webapi.uow;
using Microsoft.AspNetCore.Identity;
using NuGet.Common;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IUserServices _userServices;

    public UserController(IEmailService emailService, IUserServices userServices, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _userServices = userServices;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] CreateUserDTO model)
    {
        var user = _userServices.MatchModelToNewUser(model);
        var userCreationResult = await _unitOfWork.User.CreateAsync(user, model.Password);
        if (userCreationResult.Succeeded)
        {
            var token = await _unitOfWork.User.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "User", new { userId = user.Id, token }, Request.Scheme);

            await _emailService.SendEmailForConfirmationAsync(user, confirmationLink).ConfigureAwait(true);

            return Ok();
        }

        return BadRequest(userCreationResult.Errors);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
    {
        var user = await _unitOfWork.User.FindByEmailAsync(model.Email);
        if (user != null)
        {
            if (!await _unitOfWork.User.IsEmailConfirmedAsync(user))
            {
                return Unauthorized(new { title = "EmailNotConfirmed" });
            }
        }

        var result = await _unitOfWork.User.PasswordSignInAsync(model.Email, model.Password);
        if (result.Succeeded)
        {
            var claims = _userServices.GenerateClaims(user);
            var token = _userServices.GenerateJwtToken(claims);
            return Ok(new { token });
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpGet("ConfirmEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid user ID or token");
        }

        var user = await _unitOfWork.User.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _unitOfWork.User.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return Redirect($"{_userServices.GetReactAppRedirectAddress()}");
        }
        else
        {
            return BadRequest("Failed to confirm email");
        }
    }

    [AllowAnonymous]
    [HttpPost("forgotPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
    {
        var user = await _unitOfWork.User.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var token = await _unitOfWork.User.GeneratePasswordResetTokenAsync(user);
        var resetLink = Url.Action("ResetPassword", "User", new { userId = user.Id, token }, Request.Scheme);

        await _emailService.SendPasswordResetLinkAsync(user, resetLink);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("resetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult RedirectResetPassword(string userId, string token)
    {
        var resetPasswordPageUrl = $"{_userServices.GetReactAppRedirectAddress()}/resetPassword/{userId}/{HttpUtility.UrlEncode(token)}";

        return Redirect(resetPasswordPageUrl);
    }

    [AllowAnonymous]
    [HttpPost("resetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
    {
        var user = await _unitOfWork.User.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _unitOfWork.User.ResetPasswordAsync(user, model.Token, model.NewPassword);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _unitOfWork.User.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [Authorize]
    [HttpPut("updateUserInfo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserCredentialsDTO model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _unitOfWork.User.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        _userServices.MatchModelToExistingUser(user, model);
        var result = await _unitOfWork.User.UpdateAsync(user);
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _unitOfWork.User.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _unitOfWork.User.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (result.Succeeded)
        {
            return Ok(new { message = "Password changed successfully" });
        }
        else
        {
            return BadRequest(new { message = "Failed to change password", errors = result.Errors });
        }
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var allPartialUsersDTO = await _userServices.GetAllPartialUserDTOAsync();
        return Ok(allPartialUsersDTO);
    }

    [Authorize]
    [HttpGet("{commissionId}/availableEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvailableEmployees(Guid commissionId)
    {
        var availableUsers = await _userServices.GetAvailableEmployeesAsync(commissionId);
        if (availableUsers == null)
        {
            return NotFound("There are no available users for this commission");
        }
        return Ok(availableUsers);
    }
}
