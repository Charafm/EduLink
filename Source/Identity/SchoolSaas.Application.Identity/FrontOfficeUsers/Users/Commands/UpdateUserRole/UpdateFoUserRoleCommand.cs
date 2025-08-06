using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.UpdateUserRole
{
    public class UpdateFoUserRoleCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateFoUserRoleCommand, Result>
    {
        private readonly IStaffIdentityService _identityService;
        public UpdateUserRoleCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result> Handle(UpdateFoUserRoleCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.UpdateUserRoleAsync(request.UserId, request.RoleName);
        }
    }
}
