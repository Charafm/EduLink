using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.DataTransferObjects;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.CreateUser;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.DeleteUser;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.ResetPass;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Queries.GetUserById;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Queries.GetUsers;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OpenIddict.Validation.AspNetCore;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.ToggleUserStatus;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.UpdateUserRole;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.UpdateUser;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Commands;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Queries.GetRoles;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.PasswordForgotten;
using Microsoft.AspNetCore.Identity.Data;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.ChangePassword;
using SchoolSaas.Domain.Common.DataObjects.Common;


namespace SchoolSaas.Web.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class FoUsersController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<Domain.Common.DataObjects.Common.UserDto>>> Get(int? page, int? size,
           [FromQuery] UserCriteria userCriteria)
        {
            return await Mediator.Send(new GetFoUsersQuery { Page = page, Size = size, Criterea = userCriteria });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Common.DataObjects.Common.UserDto>> GetById(string id)
        {
            return await Mediator.Send(new GetFoUserByIdQuery { Id = id });
        }

    


        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(string id)
        {
            return await Mediator.Send(new DeleteFoUserCommand { Id = id });
        }


        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<ActionResult<Domain.Common.DataObjects.Common.UserDto>> Create(Domain.Common.DataObjects.Common.UserCreateDto userDto)
        {
            return await Mediator.Send(new CreateFoUserCommand { Data = userDto });
        }
        [AllowAnonymous]
        [HttpPut("PasswordForgotten")]
        public async Task<string> PasswordForgotten(ForgotPasswordRequest data)
        {
            return await Mediator.Send(new PasswordFogottenCommand { data = data });
        }
        [AllowAnonymous]
        [HttpPut("NewPassword")]
        public async Task<string> NewPassword(ResetPasswordRequest data)
        {
            return await Mediator.Send(new ResetPasswordRequestCommand { data = data });
        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPasswordAsync(string userId)
        {
            return await Mediator.Send(new ResetFoPasswordCommand { Id = userId });
        }

        [HttpPut("ChangePassword")]
        public async Task<Result> ChangePasswordAsync([FromBody] ChangePasswordDto Data)
        {
            return await Mediator.Send(new ChangePasswordCommand { userId = Data.userId, oldPassword= Data.oldPassword, newPassword=Data.newPassword });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Domain.Common.DataObjects.Common.UserDto>> Update(string id, UserUpdateDto userUpdateDto)
        {
            return await Mediator.Send(new UpdateFoCommand { Id = id, Data = userUpdateDto });
        }



        [HttpPut("{id}/{roleName}")]
        public async Task<ActionResult<Result>> UpdateRole(string userId, string roleName)
        {
            return await Mediator.Send(new UpdateFoUserRoleCommand { UserId = userId, RoleName = roleName });
        }


        [HttpPost("{id}/activate")]
        public async Task<ActionResult<Unit>> Activate(string id)
        {
            return await Mediator.Send(new ToggleFoUserStatusCommand { Id = id, IsActive = true });
        }


        [HttpPost("{id}/deactivate")]
        public async Task<ActionResult<Unit>> Deactivate(string id)
        {
            return await Mediator.Send(new ToggleFoUserStatusCommand { Id = id, IsActive = false });
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<List<RoleDto>>> Get()
        {
            return await Mediator.Send(new GetFoRolesQuery { });
        }
        [HttpGet("GetRoleByCriteria")]
        public async Task<ActionResult<List<RoleDto>>> GetByCriteria([FromBody] GetRoleByCriteriaDto criteria)
        {
            return await Mediator.Send(new GetFoRolesByCriteriaQuery { Criteria = criteria });
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<bool>> AddRoles([FromBody] string rolename)
        {
            return await Mediator.Send(new CreateFoRoleCommand { RoleName = rolename });
        }
        [HttpPut("UpdateRole")]
        public async Task<ActionResult<bool>> Update([FromBody] string roleId, string newRoleName)
        {
            var result = await Mediator.Send(new UpdateFoRoleCommand { RoleId = roleId, NewRoleName = newRoleName });
            return result;
        }
        [HttpDelete("DeleteRole")]
        public async Task<ActionResult<bool>> DeleteRole(string roleId)
        {
            var result = await Mediator.Send(new DeleteFoRoleCommand { RoleId = roleId });

            return result;
        }
    }
}
