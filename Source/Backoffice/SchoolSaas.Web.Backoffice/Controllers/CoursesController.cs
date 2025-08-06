using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Courses.Commands.CreateCourse;
using SchoolSaas.Application.Backoffice.Courses.Commands.DeleteCourse;
using SchoolSaas.Application.Backoffice.Courses.Commands.UpdateCourse;
using SchoolSaas.Application.Backoffice.Courses.Queries.GetCourseById;
using SchoolSaas.Application.Backoffice.Courses.Queries.GetCoursesByGrade;
using SchoolSaas.Application.Backoffice.Courses.Queries.GetPaginatedCourses;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class CoursesController : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetById(Guid id)
        {
            return await Mediator.Send(new GetCourseByIdQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CourseDTO>>> GetPaginated([FromQuery] CourseFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedCoursesQuery { DTO = filter });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CourseDTO dto)
        {
            return await Mediator.Send(new CreateCourseCommand { DTO = dto });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(Guid id, [FromBody] CourseDTO dto)
        {
            return await Mediator.Send(new UpdateCourseCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteCourseCommand { Id = id });
        }

        [HttpGet("ByGrade/{gradeLevelId}")]
        public async Task<ActionResult<List<CourseDTO>>> GetByGrade(Guid gradeLevelId)
        {
            return await Mediator.Send(new GetCoursesByGradeQuery { Id = gradeLevelId });
        }
    }
}