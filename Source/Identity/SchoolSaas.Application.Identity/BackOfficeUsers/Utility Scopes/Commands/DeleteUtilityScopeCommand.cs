using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class DeleteUtilityScopeCommand : IRequest<bool>
    {
        public Guid UtilityScopeId { get; set; }
    }

    public class DeleteUtilityScopeCommandHandler : IRequestHandler<DeleteUtilityScopeCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public DeleteUtilityScopeCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(DeleteUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.DeleteUtilityScopeAsync(request.UtilityScopeId);
        }
    }

}
