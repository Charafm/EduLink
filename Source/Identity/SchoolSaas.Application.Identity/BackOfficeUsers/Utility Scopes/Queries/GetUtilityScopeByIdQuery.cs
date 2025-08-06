using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Queries
{
    public class GetUtilityScopeByIdQuery : IRequest<UtilityScopeDto>
    {
        public Guid Id { get; set; }
    }
    public class GetUtilityScopeByIdQueryHandler : IRequestHandler<GetUtilityScopeByIdQuery, UtilityScopeDto>
    {
        private readonly IPermissionService _permissionService;

        public GetUtilityScopeByIdQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<UtilityScopeDto> Handle(GetUtilityScopeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetUtilityScopeByIdAsync(request.Id);
        }
    }
}
