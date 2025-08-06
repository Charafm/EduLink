using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Queries.GetUserById
{
    public class GetFoUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetFoUserByIdQuery, UserDto>
    {
        private readonly IStaffIdentityService _identityService;

        public GetUserByIdQueryHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserDto> Handle(GetFoUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _identityService.GetUserByIdAsync(request.Id);

            return entity;
        }
    }
}