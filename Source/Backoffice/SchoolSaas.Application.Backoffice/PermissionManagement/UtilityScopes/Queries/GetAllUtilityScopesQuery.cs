using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Queries
{
    public class GetAllUtilityScopesQuery : IRequest<List<UtilityScopeDto>>
    {
    }
    public class GetAllUtilityScopesQueryHandler : IRequestHandler<GetAllUtilityScopesQuery, List<UtilityScopeDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetAllUtilityScopesQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<UtilityScopeDto>> Handle(GetAllUtilityScopesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllUtilityScopesAsync();
        }
    }
}
