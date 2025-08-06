using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IIdentityService _identityService;
        public GetUserByEmailQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetUserByEmailAsync(request.Email);
        }
    }
}
