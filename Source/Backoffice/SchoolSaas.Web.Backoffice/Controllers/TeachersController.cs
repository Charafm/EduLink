using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Teachers.Commands.CreateTeacher;
using SchoolSaas.Application.Backoffice.Teachers.Commands.UpdateEmploymentStatus;
using SchoolSaas.Application.Backoffice.Teachers.Commands.UpdateTeacher;
using SchoolSaas.Application.Backoffice.Teachers.Queries.GetAssignedCourses;
using SchoolSaas.Application.Backoffice.Teachers.Queries.GetPaginatedTeachers;
using SchoolSaas.Application.Backoffice.Teachers.Queries.GetTeacherById;
using SchoolSaas.Application.Backoffice.Teachers.Queries.GetTeacherByUserId;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class TeachersController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreateUserRequestDto>> Create([FromBody] CreateTeacherDTO dto)
        {
            return await Mediator.Send(new CreateTeacherCommand { DTO = dto });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(Guid id, [FromBody] TeacherDTO dto)
        {
            return await Mediator.Send(new UpdateTeacherCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpPut("{id}/Status")]
        public async Task<ActionResult<bool>> UpdateEmploymentStatus(
            Guid id,
            [FromBody] TeacherStatusEnum newStatus)
        {
            return await Mediator.Send(new UpdateEmploymentStatusCommand
            {
                Id = id,
                Data = newStatus
            });
        }


        [HttpGet("{id}/Courses")]
        public async Task<ActionResult<List<CourseScheduleDTO>>> GetAssignedCourses(Guid id)
        {
            return await Mediator.Send(new GetAssignedCoursesQuery { Id = id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDTO>> GetById(Guid id)
        {
            return await Mediator.Send(new GetTeacherByIdQuery { Id = id });
        }
        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<TeacherDTO>> GetByUserId(string id)
        {
            return await Mediator.Send(new GetTeacherByUserIdQuery { userId = id });
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<TeacherDTO>>> GetPaginated(
            [FromQuery] FilterTeacherDTO filter)
        {
            return await Mediator.Send(new GetPaginatedTeachersQuery { DTO = filter });
        }
    }
}