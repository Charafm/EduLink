using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Profile;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class ProfileController : ApiController
    {
        [HttpGet("GetParentDashboard/{id}")]
        public async Task<ActionResult<ParentDashboardDTO>> GetParentDashboard(Guid id)
        {
            return await Mediator.Send(new GetParentDashboardQuery { Id = id });
        }
        [HttpGet("GetParentProfile/{id}")]
        public async Task<ActionResult<ParentProfileDTO>> GetParentProfile(string id)
        {
            return await Mediator.Send(new GetParentProfileQuery { userId = id });
        }

        [HttpGet("GetStudentDashboard/{id}")]
        public async Task<ActionResult<StudentDashboardDTO>> GetStudentDashboard(Guid id)
        {
            return await Mediator.Send(new GetStudentDashboardQuery { StudentId = id });
        }
        [HttpGet("GetStudentProfile/{id}")]
        public async Task<ActionResult<StudentProfileDTO>> GetStudentProfile(string id)
        {
            return await Mediator.Send(new GetStudentProfileQuery { userId = id });
        }
        [HttpGet("GetStaffDashboard/{id}")]
        public async Task<ActionResult<StaffDashboardDTO>> GetStaffDashboard(Guid id)
        {
            return await Mediator.Send(new GetStaffDashboardQuery { Id = id });
        }
        [HttpGet("GetStaffProfile/{id}")]
        public async Task<ActionResult<StaffProfileDTO>> GetStaffProfile(string id)
        {
            return await Mediator.Send(new GetStaffProfileQuery { userId = id });
        }
        [HttpGet("GetTeacherDashboard/{id}")]
        public async Task<ActionResult<TeacherDashboardDTO>> GetTeacherDashboard(Guid id)
        {
            return await Mediator.Send(new GetTeacherDashboardQuery { Id = id });
        }
        [HttpGet("GetTeacherProfile/{id}")]
        public async Task<ActionResult<TeacherProfileDTO>> GetTeacherProfile(string id)
        {
            return await Mediator.Send(new GetTeacherProfileQuery { userId = id });
        }
    }
}
