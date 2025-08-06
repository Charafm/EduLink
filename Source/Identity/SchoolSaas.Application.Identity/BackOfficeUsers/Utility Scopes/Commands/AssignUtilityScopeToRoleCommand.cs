using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class AssignUtilityScopeToRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public Guid UtilityScopeId { get; set; }
    }
    public class AssignUtilityScopeToRoleCommandHandler : IRequestHandler<AssignUtilityScopeToRoleCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public AssignUtilityScopeToRoleCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignUtilityScopeToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignUtilityScopeToRoleAsync(request.RoleId, request.UtilityScopeId, cancellationToken);
        }
    }
}
