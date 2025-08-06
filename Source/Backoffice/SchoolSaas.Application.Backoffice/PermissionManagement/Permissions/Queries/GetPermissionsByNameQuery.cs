using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    public class GetPermissionsByNameQuery : IRequest<List<PermissionDto>>
    {
        public string name { get; set; }
    }

    public class GetPermissionsByNameQueryHandler : IRequestHandler<GetPermissionsByNameQuery, List<PermissionDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetPermissionsByNameQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<PermissionDto>> Handle(GetPermissionsByNameQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPermissionsByNameAsync(request.name);
        }
    }
}
