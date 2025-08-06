using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    public class GetAllPermissionsQuery : IRequest<List<PermissionDto>>
    {
     
    }
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, List<PermissionDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetAllPermissionsQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllPermissionsAsync();
        }
    }
}
