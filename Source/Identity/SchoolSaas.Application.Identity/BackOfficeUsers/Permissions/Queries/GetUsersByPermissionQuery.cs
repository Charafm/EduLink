using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    
    public class GetUsersByPermissionQuery : IRequest<List<Guid>>
    {
        public string name { get; set; }
    }

    public class GetUsersByPermissionQueryHandler : IRequestHandler<GetUsersByPermissionQuery, List<Guid>>
    {
        private readonly IPermissionService _permissionService;

        public GetUsersByPermissionQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<Guid>> Handle(GetUsersByPermissionQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetUsersWithPermission(request.name);
        }
    }
}
