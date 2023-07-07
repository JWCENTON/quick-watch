using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using DTO.EquipmentDTOs;
using webapi.uow;
using DTO.Validators;

namespace webapi.Controllers;

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
    public async Task<List<Equipment>> GetAll()
    {
        return await _unitOfWork.Equipments.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Equipment> Get(Guid id)
    {
        return await _unitOfWork.Equipments.GetAsync(id);
    }

    [HttpPost]
    public async Task<Equipment> CreateEquipment([FromBody] CreateEquipmentDTO equipmentDto)
    {
        var company = await _unitOfWork.Companies.GetAsync(equipmentDto.Company.Id);

        _validator.CreateEquipmentDTOValidate(equipmentDto);
        var equipment = _mapper.Map<Equipment>(equipmentDto);
        equipment.Company = company;
        equipment.Id = Guid.NewGuid();

        await _unitOfWork.Equipments.CreateAsync(equipment);
        return equipment;
    }

}
