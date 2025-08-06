using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    
    public class GetDefaultUtilityScopesWPermissionsQuery : IRequest<List<UtilityScopeWithPermissionsDto>>
    {
        public string? Id { get; set; }
    }

    public class GetDefaultUtilityScopesWPermissionsQueryHandler : IRequestHandler<GetDefaultUtilityScopesWPermissionsQuery, List<UtilityScopeWithPermissionsDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetDefaultUtilityScopesWPermissionsQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<UtilityScopeWithPermissionsDto>> Handle(GetDefaultUtilityScopesWPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetDefaultUtilityScopesWithStatusAsync(request.Id, cancellationToken);
        }
    }
}
