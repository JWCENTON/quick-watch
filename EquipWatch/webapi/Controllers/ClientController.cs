using AutoMapper;
using DTO.ClientDTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Client.Models;
using webapi.uow;

namespace webapi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<Client>> GetAllClient()
        {
            return await _unitOfWork.Clients.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Client> GetClient(Guid id)
        {
            return await _unitOfWork.Clients.GetAsync(id);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Client> CreateClient([FromBody] CreateClientDTO clientDto)
        {
            var company = await _unitOfWork.Companies.GetAsync(clientDto.Company.Id);
            
            var client = _mapper.Map<Client>(clientDto);
            client.Company = company;
            client.Id = Guid.NewGuid();

            await _unitOfWork.Clients.CreateAsync(client);
            return client;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientDTO clientDto)
        {
            var client = await _unitOfWork.Clients.GetAsync(id);
            _mapper.Map(clientDto, client);

            await _unitOfWork.Clients.UpdateAsync(client);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
}
