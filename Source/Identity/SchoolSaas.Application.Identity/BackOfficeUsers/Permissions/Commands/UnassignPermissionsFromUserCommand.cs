using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    public class UnassignPermissionsFromUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class UnassignPermissionsFromUserCommandHandler : IRequestHandler<UnassignPermissionsFromUserCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public UnassignPermissionsFromUserCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignPermissionsFromUserCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignPermissionsFromUserAsync(new UnassignPermissionsDto
            {
                UserId = request.UserId,
                PermissionIds = request.PermissionIds
            }, cancellationToken);
        }
    }
}
