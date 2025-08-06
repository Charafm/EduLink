using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.PasswordForgotten
{
    
    public class PasswordFogottenCommand : IRequest<string>
    {
        public ForgotPasswordRequest data { get; set; }
    }

    public class PasswordFogottenCommandHandler : IRequestHandler<PasswordFogottenCommand, string>
    {
        private readonly IStaffIdentityService _identityService;

        public PasswordFogottenCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(PasswordFogottenCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ForgotPassword(request.data);
        }
    }
}
