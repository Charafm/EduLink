using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.CreateUser;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.DeleteUser;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.ResetPass;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.ToggleUserStatus;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.UpdateUser;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.UpdateUserRole;
//using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetConnectedUser;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetUserById;
using SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetUsers;
using SchoolSaas.Application.Identity.DataTransferObjects;
using SchoolSaas.Web.Common.Controllers;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Web.Identity.Controllers
{
    [Authorize(AuthenticationSchemes =  OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class UsersController : ApiController
    {
        
        [HttpGet]
        public async Task<ActionResult<PagedResult<Domain.Common.DataObjects.Common.UserDto>>> Get(int? page, int? size,
            [FromQuery] UserCriteria userCriteria)
        {
            return await Mediator.Send(new GetUsersQuery { Page = page, Size = size, Criterea = userCriteria });
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Common.DataObjects.Common.UserDto>> GetById(string id)
        {
            return await Mediator.Send(new GetUserByIdQuery { Id = id });
        }

        //[HttpGet("current")]
        //public async Task<ActionResult<User>> GetCurrent()
        //{
        //    return await Mediator.Send(new GetConnectedUserQuery());
        //}

         

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(string id)
        {
            return await Mediator.Send(new DeleteUserCommand { Id = id });
        }

         

        [HttpPost("CreateUser")]
        public async Task<ActionResult<Domain.Common.DataObjects.Common.UserDto>> Create(Domain.Common.DataObjects.Common.UserCreateDto userDto)
        {
            return await Mediator.Send(new CreateUserCommand { Data = userDto });
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPasswordAsync(string userId)
        {
            return await Mediator.Send(new ResetPasswordCommand { Id = userId });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Domain.Common.DataObjects.Common.UserDto>> Update(string id, UserUpdateDto userUpdateDto)
        {
            return await Mediator.Send(new UpdateUserCommand { Id = id, Data = userUpdateDto });
        }

         

        [HttpPut("{id}/{roleName}")]
        public async Task<ActionResult<Result>> UpdateRole(string userId, string roleName)
        {
            return await Mediator.Send(new UpdateUserRoleCommand { UserId = userId, RoleName = roleName });
        }

         
        [HttpPost("{id}/activate")]
        public async Task<ActionResult<Unit>> Activate(string id)
        {
            return await Mediator.Send(new ToggleUserStatusCommand { Id = id, IsActive = true });
        }
         

        [HttpPost("{id}/deactivate")]
        public async Task<ActionResult<Unit>> Deactivate(string id)
        {
            return await Mediator.Send(new ToggleUserStatusCommand { Id = id, IsActive = false });
        }
    }
}
