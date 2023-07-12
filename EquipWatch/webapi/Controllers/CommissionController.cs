using AutoMapper;
using Domain.Company.Models;
using Domain.Equipment.Models;
using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using webapi.uow;

namespace webapi.Controllers
{
    //[Authorize]
    [ApiController, Route("api/[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommissionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<FullCommissionDTO>>> GetAllCommissions()
        {
            var commissions = await _unitOfWork.Commissions.GetAllAsync();
            var fullCommissionDtos = _mapper.Map<List<FullCommissionDTO>>(commissions);
            return Ok(fullCommissionDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FullCommissionDTO>> GetCommission(Guid id)
        {
            var commission = await _unitOfWork.Commissions.GetAsync(id);
            var fullCommissionDto = _mapper.Map<FullCommissionDTO>(commission);
            return Ok(fullCommissionDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FullCommissionDTO>> CreateCommission([FromBody] CreateCommissionDTO commissionDto)
        {
            var commission = _mapper.Map<Domain.Commission.Models.Commission.Commission>(commissionDto);

            if (commissionDto.Company?.Id != null)
            {
                var company = await _unitOfWork.Companies.GetAsync(commissionDto.Company.Id);
                commission.Company = company;
            }

            if (commissionDto.Client?.Id != null)
            {
                var client = await _unitOfWork.Clients.GetAsync(commissionDto.Client.Id);
                commission.Client = client;
            }

            commission.Id = Guid.NewGuid();

            await _unitOfWork.Commissions.CreateAsync(commission);
            await _unitOfWork.SaveChangesAsync();

            var fullCommissionDto = _mapper.Map<FullCommissionDTO>(commission);
            return CreatedAtAction(nameof(GetCommission), new { id = fullCommissionDto.Id }, fullCommissionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCommission(Guid id, [FromBody] UpdateCommissionDTO commissionDto)
        {
            var commission = await _unitOfWork.Commissions.GetAsync(id);
_mapper.Map(commissionDto, commission);

if (commissionDto.Client?.Id != null)
{
    var client = await _unitOfWork.Clients.GetAsync(commissionDto.Client.Id);
    commission.Client = client;
}

await _unitOfWork.Commissions.UpdateAsync(commission);
await _unitOfWork.SaveChangesAsync();

return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCommission(Guid id)
        {
            await _unitOfWork.Commissions.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}