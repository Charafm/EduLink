using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class UnassignPermissionsFromUtilityScopeCommand : IRequest<bool>
    {
        public Guid UtilityScopeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class UnassignPermissionsFromUtilityScopeCommandHandler : IRequestHandler<UnassignPermissionsFromUtilityScopeCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public UnassignPermissionsFromUtilityScopeCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignPermissionsFromUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignPermissionsFromUtilityScopeAsync(request.UtilityScopeId, request.PermissionIds, cancellationToken);
        }
    }

}
