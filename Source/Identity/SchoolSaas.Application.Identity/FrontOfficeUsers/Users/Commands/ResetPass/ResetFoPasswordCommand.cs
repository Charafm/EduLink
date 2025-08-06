using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.ResetPass
{
   
    public class ResetFoPasswordCommand : IRequest<string>
    {
        public string Id { get; set; }
       
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetFoPasswordCommand, string>
    {
        private readonly IStaffIdentityService _identityService;

        public ResetPasswordCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(ResetFoPasswordCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ResetPasswordAsync(request.Id);
        }
    }
}
