using Domain.Client.Models;
using DTO.ClientDTOs;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;

namespace webapi.Controllers;


[ApiController, Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public ClientController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<List<PartialClientDTO>> GetAllClient()
    {
        //TODO implement mapping
        var data = await _unitOfWork.Clients.GetAllAsync();
        var returnData = new List<PartialClientDTO>();
        foreach (var client in data)
        {
            returnData.Add(new PartialClientDTO ()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    ContactAddress = client.ContactAddress,
                }
                );
        }
        return returnData;
    }

    [HttpGet("{id}")]
    public async Task<Client> GetClient(Guid id)
    {
        return await _unitOfWork.Clients.GetAsync(id);
    }

}
