namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        public string? UserName { get; }
        public string? PreferredUsername { get; }
        List<string>? RoleNames { get; }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(UserId);
        }
    }
}