using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Queries
{
    public class GetFoUtilityScopeByIdQuery : IRequest<UtilityScopeDto>
    {
        public Guid Id { get; set; }
    }
    public class GetUtilityScopeByIdQueryHandler : IRequestHandler<GetFoUtilityScopeByIdQuery, UtilityScopeDto>
    {
        private readonly IFoPermissionService _permissionService;

        public GetUtilityScopeByIdQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<UtilityScopeDto> Handle(GetFoUtilityScopeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenUtilityScopeByIdAsync(request.Id);
        }
    }
}
