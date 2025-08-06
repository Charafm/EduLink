using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Students.Commands.CreateStudent;
using SchoolSaas.Application.Backoffice.Students.Commands.TransitionStudentStatus;
using SchoolSaas.Application.Backoffice.Students.Commands.UpdateStudent;
using SchoolSaas.Application.Backoffice.Students.Queries.GetPaginatedStudents;
using SchoolSaas.Application.Backoffice.Students.Queries.GetStudentByUserId;
using SchoolSaas.Application.Backoffice.Students.Queries.GetStudentParents;
using SchoolSaas.Application.Backoffice.Students.Queries.GetStudentWithDetails;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class StudentsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreateStudentUserDTO>> Create([FromBody] CreateStudentDTO dto)
        {
            return await Mediator.Send(new CreateStudentCommand { DTO = dto });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(Guid id, [FromBody] StudentDTO dto)
        {
            return await Mediator.Send(new UpdateStudentCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpPut("{id}/Status")]
        public async Task<ActionResult<bool>> TransitionStatus(
            Guid id,
            [FromBody] StudentStatusTransitionDTO dto)
        {
            return await Mediator.Send(new TransitionStudentStatusCommand
            {
                Id = id,
                DTO  = dto
            });
        }

        [HttpGet("{id}/Details")]
        public async Task<ActionResult<StudentDetailDTO>> GetWithDetails(Guid id)
        {
            return await Mediator.Send(new GetStudentWithDetailsQuery { Id = id });
        }
        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<StudentDTO>> GetByUserId(string id)
        {
            return await Mediator.Send(new GetStudentByUserIdQuery { userId = id });
        }


        [HttpGet("{id}/Parents")]
        public async Task<ActionResult<PagedResult<StudentParentDTO>>> GetParents(Guid id)
        {
            return await Mediator.Send(new GetStudentParentsQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<StudentDTO>>> GetPaginated(
            [FromQuery] StudentFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedStudentsQuery { DTO = filter });
        }
    }
}