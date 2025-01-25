using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
   // [Route("api/companies")]
    [Route("api/{v:apiversion}/companies")] // commment above to use this.
    public class CompaniesV2Controllers : ControllerBase
    {
        private readonly IServiceManager _service;    
        public CompaniesV2Controllers(IServiceManager service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompanies(trackChanges: false);

            var companiesV2 = companies.Select(x => $"{x.Name} V2");

            return Ok(companiesV2);
        }
    }
}
