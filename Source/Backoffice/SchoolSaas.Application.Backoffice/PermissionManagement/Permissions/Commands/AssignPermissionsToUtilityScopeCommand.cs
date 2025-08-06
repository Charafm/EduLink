using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class AssignPermissionsToUtilityScopeCommand : IRequest<Result>
    {
        public Guid UtilityScopeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
    public class AssignPermissionsToUtilityScopeCommandHandler : IRequestHandler<AssignPermissionsToUtilityScopeCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public AssignPermissionsToUtilityScopeCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(AssignPermissionsToUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignPermissionsToUtilityScopeAsync(request.UtilityScopeId, request.PermissionIds);
        }
    }
}
