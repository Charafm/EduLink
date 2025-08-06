using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class UpdateFoUtilityScopeCommand : IRequest<bool>
    {
        public Guid id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateUtilityScopeCommandHandler : IRequestHandler<UpdateFoUtilityScopeCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public UpdateUtilityScopeCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UpdateFoUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UpdateCitizenUtilityScopeAsync(request.id, request.Title, request.Description, cancellationToken);
        }
    }
}
