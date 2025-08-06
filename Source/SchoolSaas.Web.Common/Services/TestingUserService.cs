using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Constants;

namespace SchoolSaas.Web.Common.Services
{
    public class TestingUserService : ICurrentUserService
    {
        public string? UserId { get; } = Guid.Empty.ToString();
        public string? UserName { get; } = "TestAdmin";
        public List<string> RoleNames { get; } = new List<string>() { AuthorizationConstants.Roles.SuperAdmins };
        public string? PreferredUsername => "Test Admin";
    }
}