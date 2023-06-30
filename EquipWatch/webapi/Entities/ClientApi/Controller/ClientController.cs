using Domain.Client.Models;
using Domain.Company.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities.ClientApi.Services;
using webapi.Entities.CompanyApi.Services;
using webapi.uow;

namespace webapi.Entities.ClientApi.Controller;


[ApiController, Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public ClientController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<List<Client>> GetAllClient()
    {
        return await _unitOfWork.Clients.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Client> GetClient(Guid id)
    {
        return await _clientService.Get(id);
    }

}
