using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class UnassignUtilityScopeFromRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public Guid UtilityScopeId { get; set; }
    }

    public class UnassignUtilityScopeFromRoleCommandHandler : IRequestHandler<UnassignUtilityScopeFromRoleCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public UnassignUtilityScopeFromRoleCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignUtilityScopeFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignUtilityScopeFromRoleAsync(request.RoleId, request.UtilityScopeId, cancellationToken);
        }
    }

}
