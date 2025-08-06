using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Schedules.Commands.AutoGenerateSchedules;
using SchoolSaas.Application.Backoffice.Schedules.Commands.CreateSchedule;
using SchoolSaas.Application.Backoffice.Schedules.Commands.DeleteSchedule;
using SchoolSaas.Application.Backoffice.Schedules.Commands.UpdateSchedule;
using SchoolSaas.Application.Backoffice.Schedules.Queries.CheckConflict;
using SchoolSaas.Application.Backoffice.Schedules.Queries.GetScheduleByGradeSection;
using SchoolSaas.Application.Backoffice.Schedules.Queries.GetTeacherSchedule;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class SchedulesController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] ScheduleDTO dto)
        {
            return await Mediator.Send(new CreateScheduleCommand { DTO = dto });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(Guid id, [FromBody] ScheduleDTO dto)
        {
            return await Mediator.Send(new UpdateScheduleCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteScheduleCommand { Id = id });
        }

        [HttpGet("GradeSection/{gradeSectionId}")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetByGradeSection(Guid gradeSectionId)
        {
            return await Mediator.Send(new GetScheduleByGradeSectionQuery { Id = gradeSectionId });
        }

        [HttpGet("Teacher/{teacherId}")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetTeacherSchedule(Guid teacherId)
        {
            return await Mediator.Send(new GetTeacherScheduleQuery { Id = teacherId });
        }

        [HttpPost("Generate")]
        public async Task<ActionResult<List<ScheduleDTO>>> AutoGenerate([FromBody] ScheduleConstraintsDTO constraints)
        {
            return await Mediator.Send(new AutoGenerateSchedulesCommand { DTO = constraints });
        }

        [HttpPost("CheckConflict")]
        public async Task<ActionResult<bool>> CheckConflict([FromBody] ScheduleConflictCheckDTO dto)
        {
            return await Mediator.Send(new CheckConflictQuery { DTO = dto });
        }
    }
}