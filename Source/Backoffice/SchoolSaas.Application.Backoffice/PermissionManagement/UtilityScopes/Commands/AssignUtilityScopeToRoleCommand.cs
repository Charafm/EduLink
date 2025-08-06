using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Commands
{
    public class AssignUtilityScopeToRoleCommand : IRequest<Result>
    {
        public string RoleId { get; set; }
        public Guid UtilityScopeId { get; set; }
    }
    public class AssignUtilityScopeToRoleCommandHandler : IRequestHandler<AssignUtilityScopeToRoleCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public AssignUtilityScopeToRoleCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(AssignUtilityScopeToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignUtilityScopeToRoleAsync(request.RoleId, request.UtilityScopeId);
        }
    }
}
