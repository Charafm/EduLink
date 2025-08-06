using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.DeleteUser
{
    public class DeleteFoUserCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteFoUserCommand, Result>
    {
        private readonly IStaffIdentityService _identityService;

        public DeleteUserCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(DeleteFoUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.DeleteUserAsync(request.Id);
        }
    }
}