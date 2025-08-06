using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    public class DeleteFoPermissionCommand : IRequest<bool>
    {
        public Guid PermissionId { get; set; }
    }

    public class DeletePermissionCommandHandler : IRequestHandler<DeleteFoPermissionCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public DeletePermissionCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(DeleteFoPermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.DeleteCitizenPermissionAsync(request.PermissionId);
        }
    }

}
