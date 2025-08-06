using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    public class AssignPermissionsToUserCommand : IRequest<bool>
    {
        public AssignPermissionsDto AssignPermissions { get; set; }
    }

    public class AssignPermissionsToUserCommandHandler : IRequestHandler<AssignPermissionsToUserCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public AssignPermissionsToUserCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignPermissionsToUserCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignPermissionsToUserAsync(request.AssignPermissions, cancellationToken);
        }
    }

}
