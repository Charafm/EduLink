using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Staff.Commands.AssignRole;
using SchoolSaas.Application.Backoffice.Staff.Commands.BulkCreateStaff;
using SchoolSaas.Application.Backoffice.Staff.Commands.CreateStaff;
using SchoolSaas.Application.Backoffice.Staff.Queries.GetPaginatedStaff;
using SchoolSaas.Application.Backoffice.Staff.Queries.GetStaffAuditLogs;
using SchoolSaas.Application.Backoffice.Staff.Queries.GetStaffById;
using SchoolSaas.Application.Backoffice.Staff.Queries.GetStaffByUserId;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class StaffController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreateUserRequestDto>> Create([FromBody] CreateStaffDTO dto)
        {
            return await Mediator.Send(new CreateStaffCommand { DTO = dto });
        }

        [HttpPost("Bulk")]
        public async Task<ActionResult<bool>> BulkCreate([FromBody] BulkStaffDTO dto)
        {
            return await Mediator.Send(new BulkCreateStaffCommand { DTO = dto });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDTO>> GetById(Guid id)
        {
            return await Mediator.Send(new GetStaffByIdQuery { Id = id });
        }
        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<StaffDTO>> GetByUserId(string id)
        {
            return await Mediator.Send(new GetStaffByUserIdQuery { userId = id });
        }

        [HttpPut("{id}/Role")]
        public async Task<ActionResult<bool>> AssignRole(
            Guid id,
            [FromBody] StaffRoleDTO dto)
        {
            return await Mediator.Send(new AssignRoleCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpGet("Audit/{id}")]
        public async Task<ActionResult<List<StaffAuditDTO>>> GetAuditLogs(Guid id)
        {
            return await Mediator.Send(new GetStaffAuditLogsQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<StaffDTO>>> GetPaginated(
            [FromQuery] StaffFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedStaffQuery { DTO = filter });
        }
    }
}