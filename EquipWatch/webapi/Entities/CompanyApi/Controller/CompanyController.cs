using Domain.Company.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities.CompanyApi.Services;
using webapi.uow;

namespace webapi.Entities.CompanyApi.Controller
{
    [ApiController, Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<Company> GetCompany(Guid id)
        {
            return await _unitOfWork.Companies.GetCompany(id);
        }

    }
}