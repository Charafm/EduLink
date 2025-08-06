using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class AssignPermissionsToUserCommand : IRequest<Result>
    {
        public AssignPermissionsDto AssignPermissionsDto { get; set; }
    }

    public class AssignPermissionsToUserCommandHandler : IRequestHandler<AssignPermissionsToUserCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public AssignPermissionsToUserCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(AssignPermissionsToUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignPermissionsToUserAsync(request.AssignPermissionsDto);
        }
    }
}
