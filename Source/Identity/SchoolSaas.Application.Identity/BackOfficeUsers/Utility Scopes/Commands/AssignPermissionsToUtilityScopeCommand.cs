using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class AssignPermissionsToUtilityScopeCommand : IRequest<bool>
    {
        public Guid UtilityScopeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
    public class AssignPermissionsToUtilityScopeCommandHandler : IRequestHandler<AssignPermissionsToUtilityScopeCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public AssignPermissionsToUtilityScopeCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignPermissionsToUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignPermissionsToUtilityScopeAsync(request.UtilityScopeId, request.PermissionIds, cancellationToken);
        }
    }
}
