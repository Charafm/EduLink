using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    public class AssignPermissionsToRoleCommand : IRequest<bool>
    {
        public AssignPermissionsToRoleDto Data { get; set; }
    }

    public class AssignPermissionsToRoleCommandHandler : IRequestHandler<AssignPermissionsToRoleCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public AssignPermissionsToRoleCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignPermissionsToRoleAsync(request.Data.RoleId, request.Data.Permissions, cancellationToken);
        }
    }

}
