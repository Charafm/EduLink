using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Commands
{
    public class UpdateUtilityScopeCommand : IRequest<bool>
    {
        public Guid id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateUtilityScopeCommandHandler : IRequestHandler<UpdateUtilityScopeCommand, bool>
    {
        private readonly IPermissionService _permissionService;

        public UpdateUtilityScopeCommandHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<bool> Handle(UpdateUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.UpdateUtilityScopeAsync(request.id, request.Title, request.Description, cancellationToken);
        }
    }
}
