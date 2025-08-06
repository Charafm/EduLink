using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class CreateUtilityScopeCommand : IRequest<bool>
    {
        public CreateUtilityScopeDto UtilityScope { get; set; }
    }
    public class CreateUtilityScopeCommandHandler : IRequestHandler<CreateUtilityScopeCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public CreateUtilityScopeCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(CreateUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.CreateUtilityScopeAsync(request.UtilityScope , cancellationToken);
        }
    }
}
