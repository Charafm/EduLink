using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Commands
{
    
    public class UpdateFoPermissionCommand : IRequest<bool>
    {
        public Guid id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdateFoPermissionCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public UpdatePermissionCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UpdateFoPermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UpdateCitizenPermissionAsync(request.id, request.Title, request.Description, cancellationToken);
        }
    }
}
