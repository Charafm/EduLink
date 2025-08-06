using SchoolSaas.Application.Common.Models;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ITokenValidationService
    {
        Task<bool> ValidateTokenAsync(string tokenId);
        Task<TokenInfoDto> GetTokenByIdAsync(string tokenId);
        Task<TokenInfoDto> Validate(string token);
    }
}
