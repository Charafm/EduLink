using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Attendance.Commands.DeleteAttendance;
using SchoolSaas.Application.Backoffice.Attendance.Commands.RecordAttendance;
using SchoolSaas.Application.Backoffice.Attendance.Commands.UpdateAttendance;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceById;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceSummary;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetPaginatedAttendance;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.Constants;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Web.Common.Attributes;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class AttendanceController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDTO>> GetById(Guid id)
        {
            return await Mediator.Send(new GetAttendanceByIdQuery { Id = id });
        }

        [HttpGet("Records")]
        public async Task<ActionResult<PagedResult<AttendanceDTO>>> GetPaginatedRecords([FromQuery] AttendanceFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedAttendanceQuery { DTO = filter });
        }
        // Only Teachers can use this API endpoint
        //[CustomAuthorize(AuthorizationConstants.Roles.Teacher)]
        [HttpPost]
        public async Task<ActionResult<bool>> RecordAttendance([FromBody] AttendanceDTO data)
        {
            return await Mediator.Send(new RecordAttendanceCommand { DTO = data });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateAttendance(
            Guid id,
            [FromBody] AttendanceDTO data,
            [FromQuery] AttendanceChangeReasonEnum reason)
        {
            return await Mediator.Send(new UpdateAttendanceCommand
            {
                Id = id,
                DTO = data,
                ReasonEnum = reason
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAttendance(Guid id)
        {
            return await Mediator.Send(new DeleteAttendanceCommand { Id = id });
        }

        [HttpGet("Summary/{studentId}")]
        public async Task<ActionResult<AttendanceSummaryDTO>> GetSummary(
            Guid studentId,
            [FromQuery] DateRangeDTO dateRange)
        {
            return await Mediator.Send(new GetAttendanceSummaryQuery
            {
                Id = studentId,
                DTO = dateRange
            });
        }
    }
}