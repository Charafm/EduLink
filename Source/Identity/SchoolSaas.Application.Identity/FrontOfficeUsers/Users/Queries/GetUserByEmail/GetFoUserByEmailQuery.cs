using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Queries.GetUserByEmail
{
    public class GetFoUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetFoUserByEmailQuery, UserDto>
    {
        private readonly IStaffIdentityService _identityService;
        public GetUserByEmailQueryHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserDto> Handle(GetFoUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetUserByEmailAsync(request.Email);
        }
    }
}
