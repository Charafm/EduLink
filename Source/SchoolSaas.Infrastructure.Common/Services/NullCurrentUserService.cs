using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class NullCurrentUserService : ICurrentUserService
    {
        public string UserId => string.Empty;
        public string UserName => string.Empty;
        public string? PreferredUsername => string.Empty;
        public List<string> RoleNames => new List<string>();
    }

}
