using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Constants;

namespace SchoolSaas.Web.Common.Services
{
    public class CronJobCurrentUserService : ICurrentUserService
    {
        public string? UserId { get; } = Guid.NewGuid().ToString();
        public string? UserName { get; } = "JobUser";
        public string? PreferredUsername => "Job User";
        public List<string>? RoleNames { get; } = new List<string>() { AuthorizationConstants.Roles.SuperAdmins };
    }
}