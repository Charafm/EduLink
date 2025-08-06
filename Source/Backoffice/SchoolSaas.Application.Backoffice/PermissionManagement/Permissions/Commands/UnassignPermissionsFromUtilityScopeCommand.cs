using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class UnassignPermissionsFromUtilityScopeCommand : IRequest<Result>
    {
        public Guid UtilityScopeId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
    public class UnassignPermissionsFromUtilityScopeCommandHandler : IRequestHandler<UnassignPermissionsFromUtilityScopeCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public UnassignPermissionsFromUtilityScopeCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(UnassignPermissionsFromUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnassignPermissionsFromUtilityScopeAsync(request.UtilityScopeId, request.PermissionIds);
        }
    }

}
