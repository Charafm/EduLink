using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Frontoffice.Academics;
using SchoolSaas.Application.Frontoffice.Course;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Frontoffice.Controllers
{
   
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class AcademicsController : ApiController
    {


      

        [HttpGet("GetGraderResources")]
        public async Task<ActionResult<PagedResult<GradeResourceDTO>>> GetGraderResources(ResourceFilterDTO Filter)
        {
            return await Mediator.Send(new GetGraderResourcesQuery { Filter = Filter });
        }

        [HttpGet("GetCurrentYear")]
        public async Task<ActionResult<AcademicYearDTO>> GetCurrentYear()
        {
            return await Mediator.Send(new GetCurrentYearQuery { });
        }

    }
}
