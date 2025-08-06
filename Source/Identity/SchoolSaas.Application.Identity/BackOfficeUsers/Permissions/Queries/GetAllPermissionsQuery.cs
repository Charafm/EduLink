using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    public class GetAllPermissionsQuery : IRequest<List<PermissionDto>>
    {
     
    }
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, List<PermissionDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetAllPermissionsQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetAllPermissionsAsync();
           
        }
    }
}
