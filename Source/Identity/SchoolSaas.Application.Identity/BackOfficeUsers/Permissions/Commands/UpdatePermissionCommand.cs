using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Commands
{
    
    public class UpdatePermissionCommand : IRequest<bool>
    {
        public Guid id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public UpdatePermissionCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UpdatePermissionAsync(request.id, request.Title, request.Description, cancellationToken);
        }
    }
}
