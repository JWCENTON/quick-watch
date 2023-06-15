using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models.Equipment.Services;

namespace webapi.Models.Equipment.Controller
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

        [HttpGet, Route("/{Id}")]
        public Domain.Equipment.Equipment Get(Guid id)
        {
            return _equipmentService.GetEquipment(id);
        }

        [HttpGet, Route("/all")]
        public List<Domain.Equipment.Equipment> GetAll()
        {
            return _equipmentService.GetAllEquipment();
        }
    }
}
