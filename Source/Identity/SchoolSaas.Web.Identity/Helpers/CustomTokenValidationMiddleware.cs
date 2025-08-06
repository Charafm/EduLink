using SchoolSaas.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolSaas.Web.Identity.Helpers
{
    public class CustomTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenValidationService _tokenValidationService;

        public CustomTokenValidationMiddleware(RequestDelegate next, ITokenValidationService tokenValidationService)
        {
            _next = next;
            _tokenValidationService = tokenValidationService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
          
            var requestPath = context.Request.Path.ToString();
            Console.WriteLine(requestPath);
            if (SkipValidationMiddleware.SkipValidationPaths.Contains(requestPath))
            {
              
                await _next(context);
                return;
            }
            if (context.Request.Headers.TryGetValue("Authorization", out var authorization))
            {
                var token = authorization.First().Split(" ").Last();
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                if (jwtTokenHandler.CanReadToken(token))
                {
                    var jwtToken = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;
                    var tokenId = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "oi_tkn_id")?.Value;
                    var clientId = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "client_id")?.Value;

                    if (!string.IsNullOrEmpty(tokenId))
                    {
                        var isValid = await _tokenValidationService.ValidateTokenAsync(tokenId);
                        if (!isValid)
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("Invalid or expired token");
                            return;
                        }
                        context.Items["ClientId"] = clientId;
                    }
                }
            }

            await _next(context);
        }
    }

    public static class CustomTokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomTokenValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomTokenValidationMiddleware>();
        }
    }

}
