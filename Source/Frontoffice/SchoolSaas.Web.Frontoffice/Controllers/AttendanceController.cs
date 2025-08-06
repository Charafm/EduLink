using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Frontoffice.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Frontoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class AttendanceController : ApiController
    {


        [HttpPost("SubmitAttendanceExcuse")]
        public async Task<ActionResult<bool>> SubmitAttendanceExcuse(Guid StudentId, AttendanceExcuseDTO Data) // remove Student ID
        {
            return await Mediator.Send(new SubmitAttendanceExcuseCommand { StudentId = StudentId, Excuse = Data });
        }
        //[HttpGet("GetAttendanceHistory")]
        //public async Task<ActionResult<PagedResult<AttendanceHistoryDTO>>> GetAttendanceHistory( AttendanceHistoryFilterDTO Filter)
        //{
        //    return  Mediator.Send(new GetPaginatedAttendanceHistoryQuery { Filter = Filter}).Result.Data;
        //}
        [HttpGet("GetAttendanceSummary")]
        public async Task<ActionResult<AttendanceSummaryDTO>> GetAttendanceSummary(Guid StudentId, DateRangeDTO Range)
        {
            return await Mediator.Send(new GetAttendanceSummaryQuery { StudentId = StudentId, DateRange = Range });
        }
        [HttpGet("GetAttendanceRecords")]
        public async Task<ActionResult<List<AttendanceDTO>>> GetAttendanceRecords(Guid StudentId)
        {
            return  Mediator.Send(new GetAttendanceRecordsQuery { StudentId = StudentId}).Result.Data.ToList();
        }

    }
}
