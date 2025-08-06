using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity;

namespace SchoolSaas.Infrastructure.Identity.Helpers
{
    public class TokenValidationService : ITokenValidationService
    {
        private readonly ManagerPicker _managerPicker;
        private readonly IdentityContext _context;
        private readonly FrontOfficeIdentityContext _frontOfficeIdentityContext;
        private readonly JwtTokenHandler _tokenHandler;
        public TokenValidationService(ManagerPicker managerPicker, IdentityContext context, FrontOfficeIdentityContext frontOfficeIdentity, JwtTokenHandler tokenHandler)
        {
            _managerPicker = managerPicker;
            _context = context;
            _frontOfficeIdentityContext = frontOfficeIdentity;
            _tokenHandler = tokenHandler;
        }

        public async Task<bool> ValidateTokenAsync(string tokenId)
        {
            var tokenInfo = await GetTokenInfo(tokenId);

            if (tokenInfo == null || tokenInfo.Status != "valid" || tokenInfo.ExpirationDate <= DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
        public async Task<TokenInfoDto> Validate(string token)
        {
            var tokenDecoded = _tokenHandler.DecodeToken(token);
            if (token == null)
            {
                return null;
            }
            var tokenId = tokenDecoded?.Claims.FirstOrDefault(claim => claim.Type == "oi_tkn_id")?.Value;
            return await GetTokenInfo(tokenId);
        }
        private async Task<TokenInfoDto> GetTokenInfo(string tokenId)
        {

            var token = await _context.Set<OpenIddictEntityFrameworkCoreToken>()
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == tokenId);

            if (token == null)
            {
                token = await _frontOfficeIdentityContext.Set<OpenIddictEntityFrameworkCoreToken>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => t.Id == tokenId);
                if (token == null)
                {
                    return null;
                }
            }
            if (token.ExpirationDate < DateTime.UtcNow)
            {
                token.Status = "invalid";
            }
            return new TokenInfoDto
            {
                Status = token.Status,
                ExpirationDate = token.ExpirationDate,
            };
        }
        public async Task<TokenInfoDto> GetTokenByIdAsync(string tokenId)
        {
            var token = await _context.Set<OpenIddictEntityFrameworkCoreToken>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => t.Id == tokenId);

            if (token == null)
            {
                token = await _frontOfficeIdentityContext.Set<OpenIddictEntityFrameworkCoreToken>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => t.Id == tokenId);
                if (token == null)
                {
                    return null;
                }
            }
            if (token.ExpirationDate < DateTime.UtcNow)
            {
                token.Status = "invalid";
            }
            TokenInfoDto dto = new TokenInfoDto
            {
                Status = token.Status,
                ExpirationDate = token.ExpirationDate,
            };
            return dto;
        }
    }
}
