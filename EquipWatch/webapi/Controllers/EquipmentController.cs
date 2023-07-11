using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using DTO.EquipmentDTOs;
using webapi.uow;
using DTO.Validators;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webapi.Controllers;

[Authorize]
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
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateEquipmentDTO>> CreateEquipment([FromBody] CreateEquipmentDTO equipmentDto)
    {
        var company = await _unitOfWork.Companies.GetAsync(equipmentDto.Company.Id);
        _validator.CreateEquipmentDTOValidate(equipmentDto);
        var equipment = _mapper.Map<Equipment>(equipmentDto);
        equipment.Company = company; 
        equipment.Id = Guid.NewGuid();
        await _unitOfWork.Equipments.CreateAsync(equipment);
        return CreatedAtAction(nameof(Get), new { id = equipment.Id }, _mapper.Map<FullEquipmentDTO>(equipment));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteEquipment(Guid id)
    {
        await _unitOfWork.Equipments.RemoveAsync(id);
        return NoContent();
    }
}
