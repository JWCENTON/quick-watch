using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using webapi.Entities.EquipmentApi.Services;
using webapi.uow;

namespace webapi.Entities.EquipmentApi.Controller;

[ApiController, Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public EquipmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<List<Equipment>> GetAll()
    {
        return await _unitOfWork.Equipments.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Equipment> Get(Guid id)
    {
        return await _unitOfWork.Equipments.Get(id);
    }

}
