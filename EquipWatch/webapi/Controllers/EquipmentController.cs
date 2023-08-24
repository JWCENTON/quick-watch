using AutoMapper;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Domain.Equipment.Models;
using Domain.User.Models;
using DTO.EquipmentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.uow;
using webapi.Validators;

namespace webapi.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateEquipmentDTOValidator _createValidator;
    private readonly UpdateEquipmentDTOValidator _updateValidator;

    public EquipmentController(UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        CreateEquipmentDTOValidator createEquipmentValidator,
        UpdateEquipmentDTOValidator updateEquipmentValidator)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createEquipmentValidator;
        _updateValidator = updateEquipmentValidator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PartialEquipmentDTO>> GetAll()
    {
        var data = await _unitOfWork.Equipments.GetAllAsync();
        return data.Select(equipment => _mapper.Map<PartialEquipmentDTO>(equipment)).ToList();
    }

    [HttpGet("available")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PartialEquipmentDTO>> GetAllAvailable()
    {
        var data = await _unitOfWork.Equipments.GetAllAvailableAsync();
        return data.Select(equipment => _mapper.Map<PartialEquipmentDTO>(equipment)).ToList();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Equipment> Get(Guid id)
    {
        return await _unitOfWork.Equipments.GetAsync(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FullEquipmentDTO>> CreateEquipment([FromBody] CreateEquipmentDTO equipmentDto)
    {
        var result = await _createValidator.ValidateAsync(equipmentDto);
        if (result.IsValid){
            var equipment = _mapper.Map<Equipment>(equipmentDto);

            equipment.Company = await _unitOfWork.Companies.GetAsync(equipment.CompanyId);

            equipment.Id = Guid.NewGuid();
            equipment.InWarehouse = true;
            equipment.Available = true;
            equipment.CreationTime = DateTime.Now;
            await _unitOfWork.Equipments.CreateAsync(equipment);
            var fullEquipmentDto = _mapper.Map<FullEquipmentDTO>(equipment);
            return CreatedAtAction(nameof(Get), new { id = fullEquipmentDto.Id }, fullEquipmentDto);
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEquipment(Guid id, [FromBody] UpdateEquipmentDTO equipmentDto)
    {
        var result = await _updateValidator.ValidateAsync(equipmentDto);
        if (result.IsValid)
        {
            var equipment = await _unitOfWork.Equipments.GetAsync(id);
            _mapper.Map(equipmentDto, equipment);
            if (equipmentDto.CompanyId != null)
            {
                equipment.Company = await _unitOfWork.Companies.GetAsync(equipment.CompanyId);
            }
            await _unitOfWork.Equipments.UpdateAsync(equipment);
            return NoContent();
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _unitOfWork.Equipments.RemoveAsync(id);
        return NoContent();
    }

    [HttpPost("{equipmentId}/checkout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Checkout(Guid equipmentId, [FromBody] bool warehouseDelivery)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                     ?? throw new ArgumentException("You need login to create checkout");
        var bookedEquipment = await _unitOfWork.BookedEquipment.GetCurrentBookForEquipmentWithDetailsAsync(equipmentId) 
                              ?? throw new KeyNotFoundException("No ongoing bookings found for the specified equipment");
        var equipment = bookedEquipment.EquipmentInUse.Equipment;
        if (equipment.Location.Contains("On the way to ")) { throw new ArgumentException("Equipment is already checked out"); }
        if (warehouseDelivery)
        {
            equipment.Location = "On the way to main warehouse";
            bookedEquipment.EquipmentInUse.EndTime = DateTime.Now;
            equipment.Available = true;
            bookedEquipment.EndTime = DateTime.Now;
        }
        else
        {
            equipment.Location = "On the way to " + bookedEquipment.Commission.Location;
            equipment.InWarehouse = false;
            equipment.Available = false;
        }
        await _unitOfWork.Equipments.UpdateAsync(equipment);
        var checkout = new CheckOut
        {
            Id = Guid.NewGuid(),
            Equipment = equipment,
            EquipmentId = equipment.Id,
            UserId = userId,
            WarehouseDelivery = warehouseDelivery,
            CreationTime = DateTime.Now
        };
        await _unitOfWork.BookedEquipment.UpdateAsync(bookedEquipment);
        await _unitOfWork.CheckOuts.CreateAsync(checkout);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{equipmentId}/checkin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckIn(Guid equipmentId, [FromBody] bool warehouseDelivery)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? throw new ArgumentException("You need login to create checkIn");
        var equipment = await _unitOfWork.Equipments.GetAsync(equipmentId);
        if (!equipment.Location.Contains("On the way to "))
        {
            throw new ArgumentException("Equipment is already checked in");
        }

        if (warehouseDelivery)
        {
            equipment.Location = "Main warehouse";
            equipment.InWarehouse = true;
        }
        else
        {
            var bookedEquipment = await _unitOfWork.BookedEquipment.GetCurrentBookForEquipmentWithDetailsAsync(equipmentId)
                                  ?? throw new KeyNotFoundException("No ongoing bookings found for the specified equipment");
            equipment.Location = bookedEquipment.Commission.Location;
        }

        await _unitOfWork.Equipments.UpdateAsync(equipment);
        var checkIn = new CheckIn
        {
            Id = Guid.NewGuid(),
            Equipment = equipment,
            EquipmentId = equipment.Id,
            UserId = userId,
            WarehouseDelivery = warehouseDelivery,
            CreationTime = DateTime.Now
        };
        await _unitOfWork.CheckIns.CreateAsync(checkIn);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}
