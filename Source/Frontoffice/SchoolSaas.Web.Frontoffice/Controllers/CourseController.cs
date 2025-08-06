using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Frontoffice.Course;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Frontoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class CourseController : ApiController
    {
       

        [HttpGet("GetCoursesByGradeLevel/{GradeLevelId}")]
        public async Task<ActionResult<List<CourseDTO>>> GetCoursesByGradeLevel(Guid GradeLevelId)
        {
            return  Mediator.Send(new GetCoursesByGradeLevelQuery { GradeLevelId = GradeLevelId }).Result.ToList();
        }

        [HttpGet("GetCourseDetails/{CourseId}")]
        public async Task<ActionResult<CourseDetailDTO>> GetCourseDetails(Guid CourseId)
        {
            return await Mediator.Send(new GetCourseDetailsQuery { CourseId = CourseId });
        }
    }
}
