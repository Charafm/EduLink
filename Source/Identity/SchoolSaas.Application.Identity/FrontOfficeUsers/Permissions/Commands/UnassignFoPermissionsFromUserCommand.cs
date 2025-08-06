using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    public class UnassignFoPermissionsFromUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class UnassignPermissionsFromUserCommandHandler : IRequestHandler<UnassignFoPermissionsFromUserCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public UnassignPermissionsFromUserCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignFoPermissionsFromUserCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignCitizenPermissionsFromUserAsync(new UnassignPermissionsDto
            {
                UserId = request.UserId,
                PermissionIds = request.PermissionIds
            }, cancellationToken);
        }
    }
}
