using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.Helper;
using SchoolSaas.Web.Common.Attributes;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace SchoolSaas.Web.Identity.Controllers
{
    [SkipTokenValidation]
    [ApiExplorerSettings(IgnoreApi = true)]
    [OpenApiIgnore]
    [AllowAnonymous]
    
    public class HelperController : ApiController
    {
        [SkipTokenValidation]
        [HttpGet("/token/validateById")]
        public async Task<TokenInfoDto> ValidateToken(string tokenId)
        {
            return await Mediator.Send(new TokenHelper { tokenid = tokenId });
        }
        [SkipTokenValidation]
        [HttpGet("/token/validate")]
        public async Task<TokenInfoDto> Validate()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return new TokenInfoDto { Status = "Invalid", ExpirationDate = null };
            }

            var token = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring(7) : authorizationHeader;
            return await Mediator.Send(new ValidateTokenQuery { Token = token });
        }
    }
}
