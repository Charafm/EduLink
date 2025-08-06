using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Academics.Queries.GetCurrentAcademicYear;
using SchoolSaas.Application.Backoffice.Academics.Queries.GetPaginatedSemesters;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class AcademicsController : ApiController
    {
        [HttpGet("GetCurrentYear")]
        public async Task<ActionResult<AcademicYearDTO>> GetCurrentYear()
        {
            return await Mediator.Send(new GetCurrentAcademicYeaQuery { });
        }
        [HttpGet("GetSemesters")]
        public async Task<ActionResult<PagedResult<SemesterDTO>>> GetSemester(Guid YearId, FilterSemesterDTO Filter)
        {
            return await Mediator.Send(new GetPaginatedSemestersQuery {academicYearId = YearId,filter = Filter  });
        }

    }
}
