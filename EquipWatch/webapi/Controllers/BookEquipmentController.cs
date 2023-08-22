using AutoMapper;
using Domain.BookedEquipment.Models;
using Domain.EquipmentInUse.Models;
using DTO.BookedEquipmentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.uow;
using webapi.Validators;


namespace webapi.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class BookEquipmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateBookedEquipmentDTOValidator _createBookValidator;
    public BookEquipmentController(IUnitOfWork unitOfWork, IMapper mapper, CreateBookedEquipmentDTOValidator createBookValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createBookValidator = createBookValidator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PartialBookedEquipmentDTO>> GetAll()
    {
        var data = await _unitOfWork.BookedEquipment.GetAllAsync();
        return data.Select(bookedEquipment => _mapper.Map<PartialBookedEquipmentDTO>(bookedEquipment)).ToList();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<BookedEquipment> Get(Guid id)
    {
        return await _unitOfWork.BookedEquipment.GetAsync(id);
    }

    [HttpGet("{equipmentId}/currentEquipmentBook")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FullBookedEquipmentDTO>> GetCurrentBookingForEquipment(Guid equipmentId)
    {
        var bookedEquipment = await _unitOfWork.BookedEquipment.GetCurrentBookForEquipmentAsync(equipmentId);
        var fullBookedEquipmentDto = _mapper.Map<FullBookedEquipmentDTO>(bookedEquipment);
        return Ok(fullBookedEquipmentDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FullBookedEquipmentDTO>> CreateBookedEquipment([FromBody] CreateBookedEquipmentDTO createDto)
    {
        var result = await _createBookValidator.ValidateAsync(createDto);
        if (result.IsValid)
        {
            var equipment = await _unitOfWork.Equipments.GetAsync(Guid.Parse(createDto.EquipmentId));
            var commission = await _unitOfWork.Commissions.GetAsync(Guid.Parse(createDto.CommissionId));
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? throw new ArgumentException("You need login to create book");
            if (!equipment.Available)
            {
                throw new ArgumentException("Equipment is not available");
            }

            if (equipment is { Available: true, InWarehouse: false })
            {
                equipment.Location = "On the way to " + commission.Location;
            }

            var equipmentInUse = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Equipment = equipment,
                EquipmentId = equipment.Id,
                UserId = userId,
                EndTime = createDto.EndTime
            };
            await _unitOfWork.EquipmentInUse.CreateAsync(equipmentInUse);

            var bookedEquipment = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                Commission = commission,
                CommissionId = commission.Id,
                EquipmentInUse = equipmentInUse,
                EquipmentInUseId = equipmentInUse.Id,
                IsFinished = false
            };
            await _unitOfWork.BookedEquipment.CreateAsync(bookedEquipment);
            equipment.Available = false;
            await _unitOfWork.Equipments.UpdateAsync(equipment);
            await _unitOfWork.SaveChangesAsync();

            var fullBookedEquipmentDto = _mapper.Map<FullBookedEquipmentDTO>(bookedEquipment);
            return CreatedAtAction(nameof(Get), new { id = fullBookedEquipmentDto.Id }, fullBookedEquipmentDto);
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
    public async Task<IActionResult> DeleteBookedEquipment(Guid id)
    {
        await _unitOfWork.BookedEquipment.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}
