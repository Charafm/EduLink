using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.ChangePassword
{
   
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string userId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IStaffIdentityService _identityService;

        public ChangePasswordCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ChangePasswordAsync(request.userId, request.oldPassword, request.newPassword);
        }
    }
}
