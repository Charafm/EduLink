
using Microsoft.AspNetCore.Http;
using SchoolSaas.Application.Common.Interfaces;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SchoolSaas.Web.Common.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var claimPricipal = _httpContextAccessor.HttpContext?.User;
                return claimPricipal?.FindFirstValue(Claims.Subject) ?? claimPricipal?.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
        public string UserName
        {
            get
            {
                var claimPricipal = _httpContextAccessor.HttpContext?.User;
                return claimPricipal?.FindFirstValue(Claims.Username) ?? claimPricipal?.FindFirstValue(ClaimTypes.Name);
            }
        }
        public string? PreferredUsername
        {
            get
            {
                var claimPricipal = _httpContextAccessor.HttpContext?.User;
                return claimPricipal?.FindFirstValue(Claims.Name) ?? claimPricipal?.FindFirstValue(ClaimTypes.Name);
            }
        }
        public List<string> RoleNames
        {
            get
            {
                var claimPricipal = _httpContextAccessor.HttpContext?.User;
                var roles = claimPricipal?.FindAll(Claims.Role).Select(e => e.Value).ToList();
                if (!roles.Any())
                {
                    roles = claimPricipal?.FindAll(ClaimTypes.Role).Select(e => e.Value).ToList();
                }
                return roles;
            }
        }
    }
}