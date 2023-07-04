using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using webapi.uow;

namespace webapi.Controllers;

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
        return await _unitOfWork.Equipments.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Equipment> Get(Guid id)
    {
        return await _unitOfWork.Equipments.GetAsync(id);
    }

}
