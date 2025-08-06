using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Parents.Commands.BulkCreateParents;
using SchoolSaas.Application.Backoffice.Parents.Commands.CreateParent;
using SchoolSaas.Application.Backoffice.Parents.Commands.VerifyParentIdentity;
using SchoolSaas.Application.Backoffice.Parents.Queries.CheckExistance;
using SchoolSaas.Application.Backoffice.Parents.Queries.GetAssociatedStudents;
using SchoolSaas.Application.Backoffice.Parents.Queries.GetPaginatedParents;
using SchoolSaas.Application.Backoffice.Parents.Queries.GetParentById;
using SchoolSaas.Application.Backoffice.Parents.Queries.GetParentByUserId;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class ParentsController : ApiController
    {
        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateParent([FromBody] ParentDTO dto)
        {
            return await Mediator.Send(new CreateParentCommand { DTO = dto });
        }

        [HttpPost("Bulk")]
        public async Task<ActionResult<bool>> BulkCreateParents(
            [FromBody] BulkParentDTO dto)
        {
            return await Mediator.Send(new BulkCreateParentsCommand { DTO = dto });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParentDTO>> GetParentById(Guid id)
        {
            return await Mediator.Send(new GetParentByIdQuery { Id = id });
        }
        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<ParentDTO>> GetParentByUserId(string id)
        {
            return await Mediator.Send(new GetParentByUserIdQuery { userId = id });
        }
        [HttpGet("exists")]
        public async Task<ActionResult<bool>> Exists([FromQuery] string email, CancellationToken ct)
        {
            return await Mediator.Send(new CheckExistanceQuery { email = email });
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<ParentDTO>>> GetPaginated(
            [FromQuery] ParentFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedParentsQuery { DTO = filter });
        }

        [HttpPut("{id}/Verify")]
        public async Task<ActionResult<bool>> VerifyIdentity(
            Guid id,
            [FromBody] ParentVerificationDTO dto)
        {
            return await Mediator.Send(new VerifyParentIdentityCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpGet("{id}/Students")]
        public async Task<ActionResult<PagedResult<ParsedStudentDto>>> GetAssociatedStudents(
            Guid id,
            [FromQuery] int page = 1,
            [FromQuery] int? size = 10)
        {
            return await Mediator.Send(new GetAssociatedStudentsQuery
            {
                Id = id,
                page = page,
                size = size
            });
        }
    }
}