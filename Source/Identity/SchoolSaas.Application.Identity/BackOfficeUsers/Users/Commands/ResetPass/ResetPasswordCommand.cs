using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.ResetPass
{
   
    public class ResetPasswordCommand : IRequest<string>
    {
        public string Id { get; set; }
       
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IIdentityService _identityService;

        public ResetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ResetPasswordAsync(request.Id);
        }
    }
}
