using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Queries
{
    public class GetFoUtilityScopePermissionsQuery : IRequest<List<PermissionDto>>
    {
        public Guid UtilityScopeId { get; set; }
    }
    public class GetUtilityScopePermissionsQueryHandler : IRequestHandler<GetFoUtilityScopePermissionsQuery, List<PermissionDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetUtilityScopePermissionsQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetFoUtilityScopePermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenUtilityScopePermissionsAsync(request.UtilityScopeId);
        }
    }
}
