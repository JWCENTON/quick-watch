using AutoMapper;
using Domain.WorksOn.Models;
using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;
using DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;
using webapi.Validators;

namespace webapi.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateCommissionDTOValidator _CreateValidator;
        private readonly UpdateCommissionDTOValidator _UpdateValidator;

        public CommissionController(IUnitOfWork unitOfWork, IMapper mapper, CreateCommissionDTOValidator createValidator, UpdateCommissionDTOValidator updateValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _CreateValidator = createValidator;
            _UpdateValidator = updateValidator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PartialCommissionDTO>>> GetAllCommissions()
        {
            var commissions = await _unitOfWork.Commissions.GetAllAsync();
            var fullCommissionDtos = _mapper.Map<List<PartialCommissionDTO>>(commissions);
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
            var result = await _CreateValidator.ValidateAsync(commissionDto);
            if (result.IsValid)
            {
                var commission = _mapper.Map<Domain.Commission.Models.Commission.Commission>(commissionDto);

                var company = await _unitOfWork.Companies.GetAsync(commission.CompanyId);
                commission.Company = company;

                var client = await _unitOfWork.Clients.GetAsync(commission.ClientId);
                commission.Client = client;

                commission.Id = Guid.NewGuid();

                await _unitOfWork.Commissions.CreateAsync(commission);
                await _unitOfWork.SaveChangesAsync();

                var fullCommissionDto = _mapper.Map<FullCommissionDTO>(commission);
                return CreatedAtAction(nameof(GetCommission), new { id = fullCommissionDto.Id }, fullCommissionDto);
            }
            throw new ArgumentException(result.Errors.First().ErrorMessage);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCommission(Guid id, [FromBody] UpdateCommissionDTO commissionDto)
        {
            var result = await _UpdateValidator.ValidateAsync(commissionDto);
            if (result.IsValid)
            {
                var commission = await _unitOfWork.Commissions.GetAsync(id);
                _mapper.Map(commissionDto, commission);

                if (commissionDto.ClientId != null)
                {
                    commission.Client = await _unitOfWork.Clients.GetAsync(commission.ClientId);
                }

                if (commissionDto.CompanyId != null)
                {
                    var company = await _unitOfWork.Companies.GetAsync(commission.CompanyId);
                    commission.Company = company;
                }

                await _unitOfWork.Commissions.UpdateAsync(commission);
                await _unitOfWork.SaveChangesAsync();

                return NoContent();
            }
            throw new ArgumentException(result.Errors.First().ErrorMessage);

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

        [HttpGet("{id}/equipment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<PartialEquipmentDTO>> GetEquipment(Guid id)
        {
            var equipment = await _unitOfWork.BookedEquipment.GetCommissionEquipmentAsync(id);
            return equipment.Select(equipment => _mapper.Map<PartialEquipmentDTO>(equipment)).ToList();
        }

        [HttpGet("{id}/employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<PartialUserDTO>> GetEmployees(Guid id)
        {
            var employees = await _unitOfWork.WorksOn.GetCommissionEmployeesAsync(id);
            return employees.Select(employee => _mapper.Map<PartialUserDTO>(employee)).ToList();
        }

        [HttpGet("{id}/availableEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<PartialUserDTO>> GetAvailableEmployees(Guid id)
        {
            //var assignments = _unitOfWork.WorksOn.GetAllAsync().Result.Where(assignment => assignment.CommissionId == id);
            //var assignedUsers = assignments.Select(assignment => assignment.UserId);
            var employees = _unitOfWork.Employees.GetAllAsync().Result;
            //var notAssigned = employees.Where(employee => !assignedUsers.Any(user => Guid.Parse(user) == employee.Id));
            //return notAssigned.Select(employee => _mapper.Map<PartialUserDTO>(employee)).ToList();

            return employees.Select(employee => _mapper.Map<PartialUserDTO>(employee)).ToList();
        }

        [HttpPost("{id}/employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmployee(Guid id, [FromBody] CommissionEmployeeAddDTO data)
        {
            var commission = await _unitOfWork.Commissions.GetAsync(id);

            var worksOn = new WorksOn()
            {
                Id = Guid.NewGuid(),
                Commission = commission,
                CommissionId = commission.Id,
                UserId = data.EmployeeId,
            };

            await _unitOfWork.WorksOn.CreateAsync(worksOn);

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

    }
}