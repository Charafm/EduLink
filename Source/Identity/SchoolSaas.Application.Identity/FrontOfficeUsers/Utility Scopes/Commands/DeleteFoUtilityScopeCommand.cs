using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class DeleteFoUtilityScopeCommand : IRequest<bool>
    {
        public Guid UtilityScopeId { get; set; }
    }

    public class DeleteUtilityScopeCommandHandler : IRequestHandler<DeleteFoUtilityScopeCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public DeleteUtilityScopeCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(DeleteFoUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.DeleteCitizenUtilityScopeAsync(request.UtilityScopeId);
        }
    }

}
