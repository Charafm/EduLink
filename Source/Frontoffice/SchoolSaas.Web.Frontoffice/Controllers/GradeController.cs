using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Frontoffice.Academics;
using SchoolSaas.Application.Frontoffice.Attendance;
using SchoolSaas.Application.Frontoffice.GradeBook;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Frontoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class GradeController : ApiController
    {


        
        [HttpGet("GetGradeStatistics")]
        public async Task<ActionResult<GradeStatisticsDTO>> GetGradeStatistics(GradeFilterDTO Filter)
        {
            return await Mediator.Send(new GetGradeStatisticsQuery { Filter = Filter });
        }
        [HttpGet("GetGrade")]
        public async Task<ActionResult<PagedResult<GradeDTO>>> GetGrade(GradeFilterDTO Filter)
        {
            return await Mediator.Send(new GetGradeQuery { Filter = Filter });
        }
        [HttpGet("GetGradeHistory")]
        public async Task<ActionResult<PagedResult<GradeHistoryDTO>>> GetGradeHistory(GradeHistoryFilterDTO Filter)
        {
            return await Mediator.Send(new GetGradeHistoryQuery { Filter = Filter });
        }
        [HttpGet("GetStudentGPA/{StudentId}")]
        public async Task<ActionResult<GpaDTO>> GetStudentGPA(Guid StudentId)
        {
            return await Mediator.Send(new GetStudentGPAQuery { StudentId = StudentId });
        }
        //ADD APEAL REQUEST API
    }
}
