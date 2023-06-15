using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities.Company.Services;

namespace webapi.Entities.Company.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{id}")]
        public Domain.Company.Models.Company GetCompany(Guid id)
        {
            return _companyService.GetCompany(id);
        }

    }
}