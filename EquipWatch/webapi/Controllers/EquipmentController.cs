using System.Diagnostics;
using AutoMapper;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using DTO.EquipmentDTOs;
using webapi.uow;
using DTO.Validators;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers;

//[Authorize]
[ApiController, Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly EquipmentDTOValidator _validator;

    public EquipmentController(IUnitOfWork unitOfWork, IMapper mapper, EquipmentDTOValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
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
    public async Task<ActionResult<CreateEquipmentDTO>> CreateEquipment([FromBody] CreateEquipmentDTO equipmentDto)
    {
        _validator.CreateEquipmentDTOValidate(equipmentDto);
        var equipment = _mapper.Map<Equipment>(equipmentDto);

        if (equipmentDto.Company?.Id != null)
        {
            var company = await _unitOfWork.Companies.GetAsync(equipmentDto.Company.Id);
            equipment.Company = company;
        }

        equipment.Id = Guid.NewGuid();
        await _unitOfWork.Equipments.CreateAsync(equipment);
        return CreatedAtAction(nameof(Get), new { id = equipment.Id }, _mapper.Map<FullEquipmentDTO>(equipment));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEquipment(Guid id, [FromBody] UpdateEquipmentDTO equipmentDto)
    {
        var equipment = await _unitOfWork.Equipments.GetAsync(id);
        _mapper.Map(equipmentDto, equipment);
        await _unitOfWork.Equipments.UpdateAsync(equipment);
        return NoContent();
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

    [HttpPatch("{id}/checkout")]
    public async Task<IActionResult> Checkout(Guid id, [FromBody] UpdateEquipmentLocationDTO locationDto)
    {
        var equipment = await _unitOfWork.Equipments.GetAsync(id);

        if (equipment.IsCheckedOut) { return BadRequest(); }

        equipment.IsCheckedOut = true;
        equipment.Location = locationDto.Location;
        await _unitOfWork.Equipments.UpdateAsync(equipment);

        var checkout = new CheckOut
        {
            Id = Guid.NewGuid(),
            Equipment = equipment,
            //TODO attach employee
            Time = DateTime.Now
        };

        await _unitOfWork.CheckOuts.CreateAsync(checkout);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("{id}/checkin")]
    public async Task<IActionResult> CheckIn(Guid id, [FromBody] UpdateEquipmentLocationDTO locationDto)
    {
        var equipment = await _unitOfWork.Equipments.GetAsync(id);

        if (!equipment.IsCheckedOut) { return BadRequest(); }

        equipment.IsCheckedOut = false;
        equipment.Location = locationDto.Location;
        await _unitOfWork.Equipments.UpdateAsync(equipment);

        var checkIn = new CheckIn
        {
            Id = Guid.NewGuid(),
            Equipment = equipment,
            //TODO attach employee
            Time = DateTime.Now
        };

        await _unitOfWork.CheckIns.CreateAsync(checkIn);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    try
    //    {
    //        await _unitOfWork.Equipments.RemoveAsync(id);
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
}
