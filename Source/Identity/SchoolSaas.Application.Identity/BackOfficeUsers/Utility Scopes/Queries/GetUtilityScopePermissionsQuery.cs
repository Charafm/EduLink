using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Queries
{
    public class GetUtilityScopePermissionsQuery : IRequest<List<PermissionDto>>
    {
        public Guid UtilityScopeId { get; set; }
    }
    public class GetUtilityScopePermissionsQueryHandler : IRequestHandler<GetUtilityScopePermissionsQuery, List<PermissionDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetUtilityScopePermissionsQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetUtilityScopePermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetUtilityScopePermissionsAsync(request.UtilityScopeId);
        }
    }
}
