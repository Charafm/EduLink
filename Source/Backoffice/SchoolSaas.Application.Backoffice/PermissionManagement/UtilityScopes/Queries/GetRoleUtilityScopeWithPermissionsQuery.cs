using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Queries
{
 
    public class GetRoleUtilityScopeWithPermissionsQuery : IRequest<List<UtilityScopeWithPermissionsDto>>
    {
        public string? Id { get; set; }
    }
    public class GetRoleUtilityScopeWithPermissionsQueryHandler : IRequestHandler<GetRoleUtilityScopeWithPermissionsQuery, List<UtilityScopeWithPermissionsDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetRoleUtilityScopeWithPermissionsQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<UtilityScopeWithPermissionsDto>> Handle(GetRoleUtilityScopeWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetDefaultUtilityScopesWithStatusAsync(request.Id);
        }
    }
}
