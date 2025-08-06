using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries
{
    
    public class GetFoUsersByPermissionQuery : IRequest<List<Guid>>
    {
        public string name { get; set; }
    }

    public class GetUsersByPermissionQueryHandler : IRequestHandler<GetFoUsersByPermissionQuery, List<Guid>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetUsersByPermissionQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<Guid>> Handle(GetFoUsersByPermissionQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizensWithPermission(request.name);
        }
    }
}
