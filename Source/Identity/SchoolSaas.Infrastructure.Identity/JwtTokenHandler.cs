using SchoolSaas.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using SchoolSaas.Infrastructure.Identity.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtTokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly IContextFactory _contextFactory;
    private readonly ManagerPicker _managerPicker;

    public JwtTokenHandler(IConfiguration configuration, IContextFactory contextFactory, ManagerPicker managerPicker)
    {
        _configuration = configuration;
        _contextFactory = contextFactory;
        _managerPicker = managerPicker;
    }

    private async Task<OpenIddictTokenManager<OpenIddictEntityFrameworkCoreToken>> GetTokenManager(string clientId)
    {
        var context =  _contextFactory.CreateContext(clientId);
        return _managerPicker.GetTokenManager();
    }

    public ClaimsPrincipal DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null)
            return null;

        var claims = jsonToken.Claims;
        var identity = new ClaimsIdentity(claims, "jwt");
        return new ClaimsPrincipal(identity);
    }

    public async Task<bool> RevokeUserTokensAsync(string userId, string clientId)
    {
        var tokenManager = await GetTokenManager(clientId);
        await foreach (var token in tokenManager.FindBySubjectAsync(userId))
        {
            await tokenManager.TryRevokeAsync(token);
        }
        return true;
    }

    //public async Task<string> RefreshTokenAsync(string refreshToken, string clientId)
    //{
    //    var tokenManager = await GetTokenManager(clientId);
    //    var token = await tokenManager.FindByReferenceIdAsync(refreshToken);
    //    if (token == null || token.Status != Statuses.Valid)
    //    {
    //        throw new SecurityTokenException("Invalid refresh token.");
    //    }

    //    var principal = await tokenManager.CreatePrincipal(token);
    //    var newToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
    //        issuer: "EConsulatMA",
    //        audience: "Users",
    //        claims: principal.Claims,
    //        notBefore: DateTime.UtcNow,
    //        expires: DateTime.UtcNow.AddHours(1),
    //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)));

    //    return newToken;
    //}
}
