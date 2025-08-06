using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using SchoolSaas.Web.Common.Controllers;
using System.Threading.Tasks;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class SchoolController : ApiController
    {
        private readonly ISchoolManagmentService Service;

        public SchoolController(ISchoolManagmentService service)
        {
            Service = service;
        }
        [HttpGet("GetSchoolBranches/{schoolId}")]
        public async Task<ActionResult<List<BranchDto>>> Get(Guid schoolId)
        {
            return await Service.getSchoolBranches(schoolId);
        }
    }
}
