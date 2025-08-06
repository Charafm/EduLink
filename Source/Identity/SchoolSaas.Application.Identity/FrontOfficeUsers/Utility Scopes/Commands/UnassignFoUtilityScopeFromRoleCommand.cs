using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class UnassignFoUtilityScopeFromRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public Guid UtilityScopeId { get; set; }
    }

    public class UnassignUtilityScopeFromRoleCommandHandler : IRequestHandler<UnassignFoUtilityScopeFromRoleCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public UnassignUtilityScopeFromRoleCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UnassignFoUtilityScopeFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UnassignCitizenUtilityScopeFromRoleAsync(request.RoleId, request.UtilityScopeId, cancellationToken);
        }
    }

}
