using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    public class UnassignPermissionsFromRoleCommand : IRequest<bool>
    {
        public AssignPermissionsToRoleDto Data { get; set; }
    }

    public class UnassignPermissionsFromRoleCommandHandler : IRequestHandler<UnassignPermissionsFromRoleCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public UnassignPermissionsFromRoleCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignPermissionsFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignPermissionsFromRoleAsync(request.Data.RoleId, request.Data.Permissions, cancellationToken);
        }
    }

}
