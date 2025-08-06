using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class UnassignFoPermissionsFromUtilityScopeCommand : IRequest<bool>
    {
        public Guid UtilityScopeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class UnassignPermissionsFromUtilityScopeCommandHandler : IRequestHandler<UnassignFoPermissionsFromUtilityScopeCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public UnassignPermissionsFromUtilityScopeCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignFoPermissionsFromUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignCitizenPermissionsFromUtilityScopeAsync(request.UtilityScopeId, request.PermissionIds, cancellationToken);
        }
    }

}
