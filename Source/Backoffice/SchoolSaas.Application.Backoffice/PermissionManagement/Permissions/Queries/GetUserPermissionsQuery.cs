using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    public class GetUserPermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string UserId { get; set; }
    }
    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, List<PermissionDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetUserPermissionsQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<PermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUserPermissionsAsync(request.UserId);
        }
    }
}
