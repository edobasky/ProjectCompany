﻿using Asp.Versioning;
using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using Service.Contract;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service) => _service = service;

        [HttpGet]
        // [ResponseCache(CacheProfileName = "120SecDuration")]
        [OutputCache(PolicyName = "120SecondsDuration'")]
        [EnableRateLimiting("SpecificPolicy")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        //  [ResponseCache(Duration = 60)]
        [DisableRateLimiting]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto company)
        {
            var createedCompany = await _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyById", new { id = createedCompany.Id }, createedCompany);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var companies = await _service.CompanyService.GetByIds(ids, trackChanges: false);
            return Ok(companies);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyCreateDto> companyCollection)
        {
            var result = await _service.CompanyService.CreateCompanyCollection(companyCollection);

            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service.CompanyService.DeleteCompany(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {

            await _service.CompanyService.UpdateCompany(id, company, trackChanges: true);

            return NoContent();
        }
    }
}
