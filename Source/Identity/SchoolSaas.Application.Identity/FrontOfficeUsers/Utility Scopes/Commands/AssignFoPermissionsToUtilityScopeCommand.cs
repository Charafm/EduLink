using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class AssignFoPermissionsToUtilityScopeCommand : IRequest<bool>
    {
        public Guid UtilityScopeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
    public class AssignPermissionsToUtilityScopeCommandHandler : IRequestHandler<AssignFoPermissionsToUtilityScopeCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public AssignPermissionsToUtilityScopeCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignFoPermissionsToUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignCitizenPermissionsToUtilityScopeAsync(request.UtilityScopeId, request.PermissionIds, cancellationToken);
        }
    }
}
