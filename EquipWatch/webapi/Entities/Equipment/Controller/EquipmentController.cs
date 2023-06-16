using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities.Equipment.Services;

namespace webapi.Entities.Equipment.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {

        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet("{Id}")]
        public Domain.Equipment.Equipment Get(Guid id)
        {
            return _equipmentService.GetEquipment(id);
        }

        [HttpGet("All")]
        public List<Domain.Equipment.Equipment> GetAll()
        {
            return _equipmentService.GetAllEquipment();
        }
    }
}
