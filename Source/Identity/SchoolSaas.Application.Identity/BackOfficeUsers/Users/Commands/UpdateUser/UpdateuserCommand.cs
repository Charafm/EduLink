using SchoolSaas.Application.Common.Interfaces;

using MediatR;
using SchoolSaas.Application.Identity.DataTransferObjects;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Domain.Common.DataObjects.Common.UserDto>
    {
        public string Id { get; set; }
        public UserUpdateDto Data { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Domain.Common.DataObjects.Common.UserDto>
    {
        private readonly IIdentityService _identityService;

        public UpdateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Domain.Common.DataObjects.Common.UserDto> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.UpdateUserAsync(request.Id, new Domain.Common.DataObjects.Common.UserDto()
            {
                Id = request.Id,
                FirstName = request.Data.FirstName,
                LastName = request.Data.LastName,
                PhoneNumber = request.Data.PhoneNumber,
                UserName = request.Data.UserName,
                RoleNames = request.Data.RoleNames
            });
        }
    }
}