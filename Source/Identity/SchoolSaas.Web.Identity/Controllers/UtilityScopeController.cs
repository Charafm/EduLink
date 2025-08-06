using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands;
using SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Queries;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace SchoolSaas.Web.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class UtilityScopeController : ApiController
    {
 
        [HttpGet("GetAllUtilityScopes")]
        public async Task<List<UtilityScopeDto>> GetAllUtilityScopes()
        {
            return await Mediator.Send(new GetAllUtilityScopesQuery { });
        }

    
        [HttpGet("GetUtilityScopesByCriteria")]
        public async Task<List<UtilityScopeDto>> GetUtilityScopesByCriteria([FromBody] UtilityScopeSearchCriteria criteria)
        {
            var result = await Mediator.Send(new GetUtilityScopesByCriteriaQuery { Criteria = criteria });
            return result;
        }
        [HttpGet("GetUtilityScopeById")]
        public async Task<UtilityScopeDto> GetUtilityScopeById([FromQuery] Guid id)
        {
            var utilityScope = await Mediator.Send(new GetUtilityScopeByIdQuery { Id = id });
            return utilityScope;
        }

        [HttpPost("CreateUtilityScope")]
        public async Task<bool> CreateUtilityScope([FromQuery] CreateUtilityScopeDto utilityScopeDto)
        {
            var result = await Mediator.Send(new CreateUtilityScopeCommand { UtilityScope = utilityScopeDto });
            return result;
        }

        [HttpPost("AssignUtilityScopeToRole")]
        public async Task<bool> AssignUtilityScopeToRole([FromQuery] string RoleId, Guid UtilityScopeId)
        {
            var result = await Mediator.Send(new AssignUtilityScopeToRoleCommand
            {
                RoleId = RoleId,
                UtilityScopeId = UtilityScopeId
            });
           
            return result;
        }

        [HttpGet("GetUtilityScopePermissions")]
        public async Task<List<PermissionDto>> GetUtilityScopePermissions([FromQuery] Guid utilityScopeId)
        {
            var permissions = await Mediator.Send(new GetUtilityScopePermissionsQuery { UtilityScopeId = utilityScopeId });
            return permissions;
        }

        [HttpPost("AssignPermissionsToUtilityScope")]
        public async Task<bool> AssignPermissionsToUtilityScope([FromQuery] Guid UtilityScopeId, List<Guid> PermissionIds)
        {
            return await Mediator.Send(new AssignPermissionsToUtilityScopeCommand
            {
                UtilityScopeId = UtilityScopeId,
                PermissionIds = PermissionIds
            });
        }
        [HttpPost("UnassignUtilityScopeFromRole")]
        public async Task<bool> UnassignUtilityScopeFromRole([FromQuery] string RoleId, Guid UtilityScopeId)
        {
            var result = await Mediator.Send(new UnassignUtilityScopeFromRoleCommand
            {
                RoleId=RoleId,
                UtilityScopeId=UtilityScopeId
            });
            return result ;
        }

        [HttpPost("UnassignPermissionsFromUtilityScope")]
        public async Task<bool> UnassignPermissionsFromUtilityScope([FromQuery] Guid UtilityScopeId, List<Guid> PermissionIds)
        {
            var result = await Mediator.Send(new UnassignPermissionsFromUtilityScopeCommand
            {
                UtilityScopeId = UtilityScopeId,
                PermissionIds=PermissionIds
            });
            return result;
        }
        [HttpPost("UpdateUtilityScope")]
        public async Task<bool> UpdateUtiltityScope([FromQuery] Guid UtilityId, string? NewTitle, string? NewDescription)
        {
            return await Mediator.Send(new UpdateUtilityScopeCommand
            {
                id = UtilityId,
                Title = NewTitle,
                Description = NewDescription
            });
        }
        [HttpDelete("{utilityScopeId}")]
        public async Task<ActionResult<bool>> DeleteUtilityScope(Guid utilityScopeId)
        {
            var result = await Mediator.Send(new DeleteUtilityScopeCommand { UtilityScopeId = utilityScopeId });
            return result;
        }
    }
}
