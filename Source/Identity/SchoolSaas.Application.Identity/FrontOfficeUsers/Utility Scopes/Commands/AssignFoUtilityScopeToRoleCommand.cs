using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class AssignFoUtilityScopeToRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public Guid UtilityScopeId { get; set; }
    }
    public class AssignUtilityScopeToRoleCommandHandler : IRequestHandler<AssignFoUtilityScopeToRoleCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public AssignUtilityScopeToRoleCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(AssignFoUtilityScopeToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.AssignCitizenUtilityScopeToRoleAsync(request.RoleId, request.UtilityScopeId, cancellationToken);
        }
    }
}
