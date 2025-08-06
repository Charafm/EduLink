using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    public class GetRolePermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string RoleId { get; set; }

    }
    public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, List<PermissionDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetRolePermissionsQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<PermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetRolePermissionsAsync(request.RoleId);
        }
    }
}
