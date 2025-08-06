using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SchoolSaas.Web.Common.Middlewares
{
    public class JwtQueryAccessTokenMiddleware
    {
        private readonly RequestDelegate _next;



        public JwtQueryAccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;


        }

        public Task Invoke(HttpContext context)
        {
            return BeginInvoke(context);
        }

        private async Task BeginInvoke(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString().ToLower();
            if (CollectSkipValidationPathsMiddleware.SkipValidationPaths.Contains(requestPath))
            {

                await _next(context);
                return;
            }
            StringValues values;

            if (string.IsNullOrEmpty(context.Request.Headers[HeaderNames.Authorization]) &&
                context.Request.Query.TryGetValue(WebConstants.HttpQueryAccessTokenKey, out values))
            {
                var token = values.Single();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Request.Headers[HeaderNames.Authorization] = $"{WebConstants.HttpHeaderAuthorizationType} {token}";
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = context.Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(tokenString))
            {
                try
                {
                    var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;
                    var tokenId = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "oi_tkn_id")?.Value;
                    context.Items["ClientId"] = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "client_id")?.Value;
                    if (!string.IsNullOrEmpty(tokenId))
                    {

                        var validationResult = await ValidateTokenAsync(tokenId);

                        if (validationResult == null || validationResult.Status != Statuses.Valid || validationResult.ExpirationDate <= DateTime.UtcNow)
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("Invalid or expired token");
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }

            await _next(context);
        }
        private async Task<TokenInfoDto> ValidateTokenAsync(string tokenId)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5196") };
            var response = await httpClient.GetAsync($"/token/validateById?tokenId={tokenId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TokenInfoDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    public static class JwtQueryAccessTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtQueryAccessToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtQueryAccessTokenMiddleware>();
        }
    }
}
