using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Commands
{
    public class UnassignUtilityScopeFromRoleCommand : IRequest<Result>
    {
        public string RoleId { get; set; }
        public Guid UtilityScopeId { get; set; }
    }
    public class UnassignUtilityScopeFromRoleCommandHandler : IRequestHandler<UnassignUtilityScopeFromRoleCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public UnassignUtilityScopeFromRoleCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(UnassignUtilityScopeFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnassignUtilityScopeFromRoleAsync(request.RoleId, request.UtilityScopeId);
        }
    }
}
