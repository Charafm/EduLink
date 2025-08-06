using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Queries
{
    public class GetUtilityScopeByIdQuery : IRequest<UtilityScopeDto>
    {
        public Guid Id { get; set; }
    }
    public class GetUtilityScopeByIdQueryHandler : IRequestHandler<GetUtilityScopeByIdQuery, UtilityScopeDto>
    {
        private readonly IIdentityConnectedService _service;

        public GetUtilityScopeByIdQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<UtilityScopeDto> Handle(GetUtilityScopeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUtilityScopeByIdAsync(request.Id);
        }
    }
}
