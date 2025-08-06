using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
   
    public class AssignPermissionsToRoleCommand : IRequest<Result>
    {
        public AssignPermissionsToRoleDto AssignPermissionsDto { get; set; }
    }

    public class AssignPermissionsToRoleCommandHandler : IRequestHandler<AssignPermissionsToRoleCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public AssignPermissionsToRoleCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignPermissionsToRoleAsync(request.AssignPermissionsDto);
        }
    }
}
