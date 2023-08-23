using AutoMapper;
using Domain.Client.Models;
using DTO.ClientDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;
using webapi.Validators;

namespace webapi.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateClientDTOValidator _createValidator;
    private readonly UpdateClientDTOValidator _updateValidator;
    public ClientController(IUnitOfWork unitOfWork, IMapper mapper, CreateClientDTOValidator createValidator, UpdateClientDTOValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PartialClientDTO>> GetAllClient()
    {
        var data = await _unitOfWork.Clients.GetAllAsync();
        return data.Select(client => _mapper.Map<PartialClientDTO>(client)).ToList();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Client> GetClient(Guid id)
    {
        return await _unitOfWork.Clients.GetAsync(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FullClientDTO>> CreateClient([FromBody] CreateClientDTO clientDto)
    {
        var result = await _createValidator.ValidateAsync(clientDto);
        if (result.IsValid)
        {
            var client = _mapper.Map<Client>(clientDto);

            var company = await _unitOfWork.Companies.GetAsync(client.CompanyId);
            client.Company = company;
            client.Id = Guid.NewGuid();

            await _unitOfWork.Clients.CreateAsync(client);
            var fullClientDto = _mapper.Map<FullClientDTO>(client);
            return CreatedAtAction(nameof(GetClient), new { id = fullClientDto.Id }, fullClientDto);
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientDTO clientDto)
    {
        var result = await _updateValidator.ValidateAsync(clientDto);
        if (result.IsValid)
        {
            var client = await _unitOfWork.Clients.GetAsync(id);
            _mapper.Map(clientDto, client);

            if (clientDto.CompanyId != null)
            {
                client.Company = await _unitOfWork.Companies.GetAsync(client.CompanyId);
            }

            await _unitOfWork.Clients.UpdateAsync(client);
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        await _unitOfWork.Clients.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}
