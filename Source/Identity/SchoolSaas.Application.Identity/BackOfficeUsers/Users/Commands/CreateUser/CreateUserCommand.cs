using SchoolSaas.Application.Common.Interfaces;

using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Domain.Common.DataObjects.Common.UserDto>
    {
        public Domain.Common.DataObjects.Common.UserCreateDto Data { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Domain.Common.DataObjects.Common.UserDto>
    {
        protected readonly IIdentityService _identityService;
        protected readonly ICurrentUserService _currentUserService;

        public CreateUserCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<Domain.Common.DataObjects.Common.UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateUserAsync(new Domain.Common.DataObjects.Common.UserDto()
            {
                UserName = request.Data.UserName,
                Email = request.Data.Email,
                FirstName = request.Data.FirstName,
                LastName = request.Data.LastName,
                RoleNames = new List<string>() { request.Data.RoleName },
                PhoneNumber= request.Data.PhoneNumber,
                Function =null
            }, request.Data.Password);
        }
    }

}