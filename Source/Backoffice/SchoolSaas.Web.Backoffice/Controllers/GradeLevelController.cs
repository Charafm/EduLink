using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Attendance.Commands.DeleteAttendance;
using SchoolSaas.Application.Backoffice.Attendance.Commands.RecordAttendance;
using SchoolSaas.Application.Backoffice.Attendance.Commands.UpdateAttendance;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceById;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceSummary;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetPaginatedAttendance;
using SchoolSaas.Application.Backoffice.GradeLevel.Commands.CreateLevel;
using SchoolSaas.Application.Backoffice.GradeLevel.Commands.DeleteLevel;
using SchoolSaas.Application.Backoffice.GradeLevel.Commands.UpdateLevel;
using SchoolSaas.Application.Backoffice.GradeLevel.Queries.GetLevelByEducationalStage;
using SchoolSaas.Application.Backoffice.GradeLevel.Queries.GetLevelById;
using SchoolSaas.Application.Backoffice.GradeLevel.Queries.GetLevelByName;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class GradeLevelController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeLevelDTO>> GetById(Guid id)
        {
            return await Mediator.Send(new GetLevelByIdQuery { Id = id });
        }

        [HttpGet("GetByEducationalStage")]
        public async Task<ActionResult<PagedResult<GradeLevelDTO>>> GetLevelByES([FromQuery] EducationalStageEnum Stage, int page , int? size)
        {
            return await Mediator.Send(new GetLevelByEducationalStageQuery { Stage = Stage, page = page, size = size });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] GradeLevelDTO data)
        {
            return await Mediator.Send(new CreateLevelCommand { DTO = data });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(
            Guid id,
            [FromBody] GradeLevelDTO data)
          
        {
            return await Mediator.Send(new UpdateLevelCommand
            {
                Id = id,
                DTO = data,
              
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteLevelCommand { Id = id });
        }

        [HttpGet("getByName")]
        public async Task<ActionResult<GradeLevelDTO>> GetByName(
            string Name)
        {
            return await Mediator.Send(new GetLevelByNameQuery
            {
               Name = Name
            });
        }
    }
}
