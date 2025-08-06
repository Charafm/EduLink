using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    public class CreateFoPermissionCommand : IRequest<bool>
    {
        public CreatePermissionDto Permission { get; set; }
    }

    public class CreatePermissionCommandHandler : IRequestHandler<CreateFoPermissionCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public CreatePermissionCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(CreateFoPermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.CreateCitizenPermissionAsync(request.Permission, cancellationToken);
        }
    }

}
