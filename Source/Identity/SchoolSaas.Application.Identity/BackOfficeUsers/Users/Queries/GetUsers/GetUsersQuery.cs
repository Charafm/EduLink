using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<PagedResult<UserDto>>
    {
        public UserCriteria Criterea { get; set; } = new UserCriteria();
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 25;
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public GetUsersQueryHandler(IIdentityService context, ICurrentUserService currentUserService)
        {
            _identityService = context;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var req = request;
            return await _identityService.GetUsersAsync(request.Page, request.Size, req.Criterea);
        }
    }
}