using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    public class GetUtilityScopePermissionsQuery : IRequest<List<PermissionDto>>
    {
        public Guid UtilityScopeId { get; set; }
    }
    public class GetUtilityScopePermissionsQueryHandler : IRequestHandler<GetUtilityScopePermissionsQuery, List<PermissionDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetUtilityScopePermissionsQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<PermissionDto>> Handle(GetUtilityScopePermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUtilityScopePermissionsAsync(request.UtilityScopeId);
        }
    }
}
