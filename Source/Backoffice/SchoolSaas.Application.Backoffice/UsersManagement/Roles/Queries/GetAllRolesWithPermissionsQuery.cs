using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.UsersManagement.Roles.Queries
{
  
    public class GetAllRolesWithPermissionsQuery : IRequest<AllRolesPermissionsDto>
    {
     
    }
    public class GetAllRolesWithPermissionsQueryHandler : IRequestHandler<GetAllRolesWithPermissionsQuery, AllRolesPermissionsDto>
    {
        private readonly IIdentityConnectedService _identityService;

        public GetAllRolesWithPermissionsQueryHandler(IIdentityConnectedService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AllRolesPermissionsDto> Handle(GetAllRolesWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetAllRolesWithPermissionsAsync();
        }
    }
}
