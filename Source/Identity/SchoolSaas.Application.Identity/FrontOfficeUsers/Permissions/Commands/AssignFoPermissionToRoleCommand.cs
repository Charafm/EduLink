using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    public class AssignFoPermissionToRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class AssignPermissionsToRoleCommandHandler : IRequestHandler<AssignFoPermissionToRoleCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public AssignPermissionsToRoleCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignFoPermissionToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignCitizenPermissionsToRoleAsync(request.RoleId, request.PermissionIds, cancellationToken);
        }
    }

}
