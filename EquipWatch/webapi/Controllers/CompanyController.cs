using Domain.Company.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;

namespace webapi.Controllers
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
            return await _unitOfWork.Companies.GetCompanyAsync(id);
        }

    }
}