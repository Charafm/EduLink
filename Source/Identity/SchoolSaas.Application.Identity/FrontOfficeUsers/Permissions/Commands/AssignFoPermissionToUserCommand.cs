using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    public class AssignFoPermissionToUserCommand : IRequest<bool>
    {
        public AssignPermissionsDto AssignPermissions { get; set; }
    }

    public class AssignCitizenPrmToUserCommandHandler : IRequestHandler<AssignFoPermissionToUserCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public AssignCitizenPrmToUserCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignFoPermissionToUserCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignCitizenPermissionsToUserAsync(request.AssignPermissions, cancellationToken);
        }
    }

}
