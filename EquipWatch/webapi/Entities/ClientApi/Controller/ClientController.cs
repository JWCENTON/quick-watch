using Domain.Client.Models;
using Domain.Company.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities.CompanyApi.Services;

namespace webapi.Entities.ClientApi.Controller;


[ApiController, Route("api/[controller]")]
public class ClientController : ControllerBase
{

    private readonly IclientService _clientService;

    public ClientController(IclientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<List<Client>> GetAllClient()
    {
        return await _clientService.GetAll();
    }

}
