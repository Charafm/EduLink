using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.Helper
{
  
    public class ValidateTokenQuery : IRequest<TokenInfoDto>
    {
        public string Token { get; set; }
    }
    public class ValidateTokenQueryHandler : IRequestHandler<ValidateTokenQuery, TokenInfoDto>
    {
        private readonly ITokenValidationService _Service;

        public ValidateTokenQueryHandler(ITokenValidationService Service)
        {
            _Service = Service;
        }

        public async Task<TokenInfoDto> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
        {
            return await _Service.Validate(request.Token);

        }
    }
}
