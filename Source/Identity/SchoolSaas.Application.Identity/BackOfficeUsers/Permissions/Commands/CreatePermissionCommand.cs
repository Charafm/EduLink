using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;
namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    public class CreatePermissionCommand : IRequest<bool>
    {
        public CreatePermissionDto Permission { get; set; }
    }

    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public CreatePermissionCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.CreatePermissionAsync(request.Permission, cancellationToken);
        }
    }

}
