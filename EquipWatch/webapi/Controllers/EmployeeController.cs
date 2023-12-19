using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.uow;

namespace webapi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeServices _employeeServices;

    public EmployeeController(IUnitOfWork unitOfWork, IEmployeeServices employeeServices)
    {
        _unitOfWork = unitOfWork;
        _employeeServices = employeeServices;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEmployeeByIdAsync(string id)
    {
        var user = await _unitOfWork.User.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    [HttpGet("{id}/equipments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEquipmentByEmployeeIdAsync(string id)
    {
        var equipmentByEmployeeId = await _employeeServices.GetEquipmentInUseByUserIdAsync(id);
        if (equipmentByEmployeeId == null)
        {
            return NotFound("No equipment assigned to the user");
        }
        return Ok(equipmentByEmployeeId);
    }
}