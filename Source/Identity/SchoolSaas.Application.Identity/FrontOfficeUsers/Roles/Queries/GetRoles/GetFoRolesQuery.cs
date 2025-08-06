using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Queries.GetRoles
{
    public class GetFoRolesQuery : IRequest<List<RoleDto>>
    {
    }

    public class GetRolesQueryHandler : IRequestHandler<GetFoRolesQuery, List<RoleDto>>
    {
        private readonly IStaffIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public GetRolesQueryHandler(IStaffIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<List<RoleDto>> Handle(GetFoRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetRolesAsync();
            return roles;
        }
    }
}