using AutoMapper;
using Domain.Client.Models;
using DTO.ClientDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;

namespace webapi.Controllers;

[Authorize]
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
    public async Task<List<PartialClientDTO>> GetAllClient()
    {
        var data = await _unitOfWork.Clients.GetAllAsync();
        return data.Select(client => _mapper.Map<PartialClientDTO>(client)).ToList();
    }

    [HttpGet("{id}")]
    public async Task<Client> GetClient(Guid id)
    {
        return await _unitOfWork.Clients.GetAsync(id);
    }

}
