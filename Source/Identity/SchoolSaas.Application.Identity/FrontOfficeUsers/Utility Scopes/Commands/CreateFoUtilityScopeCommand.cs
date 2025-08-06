using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Commands
{
    public class CreateFoUtilityScopeCommand : IRequest<bool>
    {
        public CreateUtilityScopeDto UtilityScope { get; set; }
    }
    public class CreateUtilityScopeCommandHandler : IRequestHandler<CreateFoUtilityScopeCommand, bool>
    {
        private readonly IFoPermissionService _permissionService;

        public CreateUtilityScopeCommandHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(CreateFoUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.CreateCitizenUtilityScopeAsync(request.UtilityScope , cancellationToken);
        }
    }
}
