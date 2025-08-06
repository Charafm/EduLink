using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IIdentityService _identityService;

        public GetUserByIdQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _identityService.GetUserByIdAsync(request.Id);

            return entity;
        }
    }
}