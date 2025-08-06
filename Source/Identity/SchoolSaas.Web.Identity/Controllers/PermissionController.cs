using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands;
using SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace SchoolSaas.Web.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class PermissionController : ApiController
    {

        [HttpGet("GetAllPermissions")]
        public async Task<List<PermissionDto>> GetAllPermissions()
        {
            return await Mediator.Send(new GetAllPermissionsQuery { });
        }


        [AllowAnonymous]
        [HttpGet("GetUsersByPermission")]
        public async Task<List<Guid>> GetUsersByPermission(string PermissionName)
        {
            return await Mediator.Send(new GetUsersByPermissionQuery {name = PermissionName });
        }


        [HttpGet("GetPermissionById")]
        public async Task<PermissionDto> GetPermissionById([FromQuery] Guid id)
        {
            return await Mediator.Send(new GetPermissionByIdQuery { Id = id });
        }
        [HttpGet("GetPermissionsByCriteria")]
        public async Task<List<PermissionDto>> GetPermissionsByCriteria([FromBody] string name)
        {
            return await Mediator.Send(new GetPermissionsByNameQuery { name = name });
        }
        [HttpPost("CreatePermission")]
        public async Task<bool> CreatePermission([FromQuery] CreatePermissionDto permissionDto)
        {
            return await Mediator.Send(new CreatePermissionCommand { Permission = permissionDto });
        }

        [HttpPost("AssignPermissionsToUser")]
        public async Task<bool> AssignPermissionsToUser([FromBody] AssignPermissionsDto assignPermissionsDto)
        {
            return await Mediator.Send(new AssignPermissionsToUserCommand { AssignPermissions = assignPermissionsDto });
        }
        [HttpPost("AssignPermissionsToRole")]
        public async Task<bool> AssignPermissionsToRole([FromBody] AssignPermissionsToRoleDto assignPermissionsDto)
        {
            return await Mediator.Send(new AssignPermissionsToRoleCommand { Data = assignPermissionsDto });
        }

        [HttpGet("GetUserPermissions")]
        public async Task<List<PermissionDto>> GetUserPermissions([FromQuery] string userId)
        {
            return await Mediator.Send(new GetUserPermissionsQuery { UserId = userId });
        }

        [HttpGet("GetRolePermissions")]
        public async Task<List<PermissionDto>> GetRolePermissions([FromQuery] string roleId)
        {
            return await Mediator.Send(new GetRolePermissionsQuery { RoleId = roleId });
        }
        [HttpGet("GetDefaultPermissions")]
        public async Task<List<UtilityScopeWithPermissionsDto>> GetDefaultUtilityScopesWithPermissions([FromQuery] string? roleId)
        {
            return await Mediator.Send(new GetDefaultUtilityScopesWPermissionsQuery { Id = roleId });
        }
        [HttpPost("UnassignPermissionsFromUser")]
        public async Task<bool> UnassignPermissionsFromUser([FromQuery]   string UserId,List<Guid> PermissionIds)
        {
            var result = await Mediator.Send(new UnassignPermissionsFromUserCommand { 
            UserId = UserId,
            PermissionIds = PermissionIds
            });
            return result ;
        }

        [HttpPost("UnassignPermissionsFromRole")]
        public async Task<bool> UnassignPermissionsFromRole([FromQuery] AssignPermissionsToRoleDto Data)
        {
            return await Mediator.Send(new UnassignPermissionsFromRoleCommand
            {
                Data = Data
            });
        }
        [HttpPost("UpdatePermission")]
        public async Task<bool> UpdatePermission([FromQuery] Guid PermissionId, string? NewTitle, string? NewDescription)
        {
            return await Mediator.Send(new UpdatePermissionCommand
            {
                id = PermissionId,
                Title = NewTitle,
                Description = NewDescription
            });
        }
        [HttpDelete("DeletePermission")]
        public async Task<ActionResult<bool>> DeletePermission(Guid permissionId)
        {
            var result = await Mediator.Send(new DeletePermissionCommand { PermissionId = permissionId });

            return result;
        }
    }
}
