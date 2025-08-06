using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries
{
    public class GetAllFoPermissionsQuery : IRequest<List<PermissionDto>>
    {
     
    }
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllFoPermissionsQuery, List<PermissionDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetAllPermissionsQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetAllFoPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetAllCitizenPermissionsAsync();
           
        }
    }
}
