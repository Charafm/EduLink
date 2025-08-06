using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.PasswordForgotten
{
    
    public class ResetPasswordRequestCommand : IRequest<string>
    {
        public ResetPasswordRequest data { get; set; }

    }

    public class ResetPasswordRequestCommandHandler : IRequestHandler<ResetPasswordRequestCommand, string>
    {
        private readonly IStaffIdentityService _identityService;

        public ResetPasswordRequestCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(ResetPasswordRequestCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ResetPasswordRequest(request.data);
        }
    }
}
