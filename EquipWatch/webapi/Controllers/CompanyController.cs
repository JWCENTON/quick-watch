using AutoMapper;
using Domain.Company.Models;
using Domain.Equipment.Models;
using DTO.CompanyDTOs;
using DTO.EquipmentDTOs;
using DTO.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;
using static CompanyDTOValidator;

namespace webapi.Controllers
{
    //[Authorize]
    [ApiController, Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateCompanyDTOValidator _validator1;
        private readonly CreateCompanyDTOValidator _validator2;
        private readonly IEnumerable<IValidator> _validators;

        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper,
            CreateCompanyDTOValidator validator1,
            UpdateCompanyDTOValidator validator2,
            IEnumerable<IValidator> validators)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator1 = validator2;
            _validator2 = validator1;
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
            var createCompanyValidator = new UpdateCompanyDTOValidator();
            createCompanyValidator
            //var user = 
            _validator.
            
            var company = _mapper.Map<Company>(companyDto);
            //company.Owner = user;
            company.Id = Guid.NewGuid();

            await _unitOfWork.Companies.CreateAsync(company);
            return company;
        }
    }
}