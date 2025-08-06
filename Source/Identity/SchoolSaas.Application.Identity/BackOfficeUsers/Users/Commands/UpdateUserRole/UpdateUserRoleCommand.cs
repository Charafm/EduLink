using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public UpdateUserRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.UpdateUserRoleAsync(request.UserId, request.RoleName);
        }
    }
}
