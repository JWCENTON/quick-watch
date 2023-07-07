using AutoMapper;
using Domain.Company.Models;
using Domain.Equipment.Models;
using DTO.CompanyDTOs;
using DTO.EquipmentDTOs;
using DTO.Validators;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;

namespace webapi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CompanyDTOValidator _validator;

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper, CompanyDTOValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<Company> GetCompany(Guid id)
        {
            return await _unitOfWork.Companies.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<Company>> GetAllCompanies()
        {
            return await _unitOfWork.Companies.GetAllAsync();
        }

        [HttpPost]
        public async Task<Company> CreateCompany([FromBody] CreateCompanyDTO companyDto)
        {
            //var user = 
            _validator.CreateCompanyDTOValidate(companyDto);
            var company = _mapper.Map<Company>(companyDto);
            //company.Owner = user;
            company.Id = Guid.NewGuid();

            await _unitOfWork.Companies.CreateAsync(company);
            return company;
        }
    }
}