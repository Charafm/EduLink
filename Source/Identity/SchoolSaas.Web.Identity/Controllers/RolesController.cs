using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.BackOfficeUsers.Roles.Commands;
using SchoolSaas.Application.Identity.BackOfficeUsers.Roles.Queries.GetRoles;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace SchoolSaas.Web.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class RolesController : ApiController
    {
        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<List<RoleDto>>> Get()
        {
            return await Mediator.Send(new GetRolesQuery { });
        }
        [HttpGet("GetRoleByCriteria")]
        public async Task<ActionResult<List<RoleDto>>> GetByCriteria([FromBody]GetRoleByCriteriaDto criteria)
        {
            return await Mediator.Send(new GetRolesByCriteriaQuery {Criteria= criteria });
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<bool>> AddRoles([FromBody] string rolename)
        {
            return await Mediator.Send(new CreateRoleCommand { RoleName = rolename});
        }
        [HttpPut("UpdateRole")]
        public async Task<ActionResult<bool>> UpdateRole([FromBody] string roleId,  string newRoleName)
        {
            var result = await Mediator.Send(new UpdateRoleCommand { RoleId = roleId, NewRoleName = newRoleName });
            return result;
        }
        [HttpDelete("DeleteRole")]
        public async Task<ActionResult<bool>> DeleteRole(string roleId)
        {
            var result = await Mediator.Send(new DeleteRoleCommand { RoleId = roleId });

            return result;
        }
    }
}
