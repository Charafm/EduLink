using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class UnassignPermissionsFromUserCommand : IRequest<Result>
    {
        public UnassignPermissionsDto UnassignPermissionsDto { get; set; }
    }
    public class UnassignPermissionsFromUserCommandHandler : IRequestHandler<UnassignPermissionsFromUserCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public UnassignPermissionsFromUserCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(UnassignPermissionsFromUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnassignPermissionsFromUserAsync(request.UnassignPermissionsDto);
        }
    }
}
