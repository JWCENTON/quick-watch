using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using webapi.Entities.EquipmentApi.Services;

namespace webapi.Entities.EquipmentApi.Controller;

[ApiController, Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public async Task<List<Equipment>> GetAll()
    {
        return await _equipmentService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Equipment> Get(Guid id)
    {
        return await _equipmentService.Get(id);
    }

}
