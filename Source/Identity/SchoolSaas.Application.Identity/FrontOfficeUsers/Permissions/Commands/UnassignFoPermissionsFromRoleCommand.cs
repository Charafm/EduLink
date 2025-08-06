using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    public class UnassignFoPermissionsFromRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class UnassignPermissionsFromRoleCommandHandler : IRequestHandler<UnassignFoPermissionsFromRoleCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public UnassignPermissionsFromRoleCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignFoPermissionsFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignCitizenPermissionsFromRoleAsync(request.RoleId, request.PermissionIds, cancellationToken);
        }
    }

}
