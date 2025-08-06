using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace SchoolSaas.Web.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class FoPermissionController : ApiController
    {
        [HttpGet("GetAllPermissions")]
        public async Task<List<PermissionDto>> GetAllPermissions()
        {
            return await Mediator.Send(new GetAllFoPermissionsQuery { });
        }

        [AllowAnonymous]
        [HttpGet("GetUsersByPermission")]
        public async Task<List<Guid>> GetUsersByPermission(string PermissionName)
        {
            return await Mediator.Send(new GetFoUsersByPermissionQuery { name = PermissionName });
        }


        [HttpGet("GetPermissionById")]
        public async Task<PermissionDto> GetPermissionById([FromQuery] Guid id)
        {
            return await Mediator.Send(new GetFoPermissionByIdQuery { Id = id });
        }
        [HttpGet("GetPermissionsByCriteria")]
        public async Task<List<PermissionDto>> GetPermissionsByCriteria([FromBody] string name)
        {
            return await Mediator.Send(new GetFoPermissionsByNameQuery { name = name });
        }
        [HttpPost("CreatePermission")]
        public async Task<bool> CreatePermission([FromQuery] CreatePermissionDto permissionDto)
        {
            return await Mediator.Send(new CreateFoPermissionCommand { Permission = permissionDto });
        }

        [HttpPost("AssignPermissionsToUser")]
        public async Task<bool> AssignPermissionsToUser([FromQuery] AssignPermissionsDto assignPermissionsDto)
        {
            return await Mediator.Send(new AssignFoPermissionToUserCommand { AssignPermissions = assignPermissionsDto });
        }
        [HttpPost("AssignPermissionsToRole")]
        public async Task<bool> AssignPermissionsToRole([FromQuery] string roleId, List<Guid> permissions)
        {
            return await Mediator.Send(new AssignFoPermissionToRoleCommand { RoleId = roleId, PermissionIds = permissions });
        }

        [HttpGet("GetUserPermissions")]
        public async Task<List<PermissionDto>> GetUserPermissions([FromQuery] string userId)
        {
            return await Mediator.Send(new GetFoPermissionsQuery { UserId = userId });
        }

        [HttpGet("GetRolePermissions")]
        public async Task<List<PermissionDto>> GetRolePermissions([FromQuery] string roleId)
        {
            return await Mediator.Send(new GetFoRolePermissionsQuery { RoleId = roleId });
        }
        [HttpPost("UnassignPermissionsFromUser")]
        public async Task<bool> UnassignPermissionsFromUser([FromQuery] string UserId, List<Guid> PermissionIds)
        {
            var result = await Mediator.Send(new UnassignFoPermissionsFromUserCommand
            {
                UserId = UserId,
                PermissionIds = PermissionIds
            });
            return result;
        }

        [HttpPost("UnassignPermissionsFromRole")]
        public async Task<bool> UnassignPermissionsFromRole([FromQuery] string roleID, List<Guid> PermissionIds)
        {
            return await Mediator.Send(new UnassignFoPermissionsFromRoleCommand
            {
                RoleId = roleID,
                PermissionIds = PermissionIds
            });
        }
        [HttpPost("UpdatePermission")]
        public async Task<bool> UpdatePermission([FromQuery] Guid PermissionId, string? NewTitle, string? NewDescription)
        {
            return await Mediator.Send(new UpdateFoPermissionCommand
            {
                id = PermissionId,
                Title = NewTitle,
                Description = NewDescription
            });
        }
        [HttpDelete("DeletePermission")]
        public async Task<ActionResult<bool>> DeletePermission(Guid permissionId)
        {
            var result = await Mediator.Send(new DeleteFoPermissionCommand { PermissionId = permissionId });

            return result;
        }
    }
}
