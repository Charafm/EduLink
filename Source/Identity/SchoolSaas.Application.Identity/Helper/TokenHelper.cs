using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.Helper
{
  
    public class TokenHelper : IRequest<TokenInfoDto>
    {
        public string tokenid { get; set; }
    }
    public class TokenHelperHandler : IRequestHandler<TokenHelper, TokenInfoDto>
    {
        private readonly ITokenValidationService _Service;

        public TokenHelperHandler(ITokenValidationService Service)
        {
            _Service = Service;
        }

        public async Task<TokenInfoDto> Handle(TokenHelper request, CancellationToken cancellationToken)
        {
            return await _Service.GetTokenByIdAsync(request.tokenid);

        }
    }
}
