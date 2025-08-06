using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    public class DeletePermissionCommand : IRequest<bool>
    {
        public Guid PermissionId { get; set; }
    }

    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public DeletePermissionCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.DeletePermissionAsync(request.PermissionId);
        }
    }

}
