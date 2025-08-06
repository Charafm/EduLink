using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Web.IdentityFrontal.Models
{
    public static class IdentityTypeExtensions
    {
        public static string BasePath(this IdentityType type)
            => type == IdentityType.FrontOffice
                ? "api/fousers"
                : "api/users";

        public static string RolesPath(this IdentityType type)
            => type == IdentityType.FrontOffice
                ? "api/fousers"
                : "api/roles";

        public static string PermissionPath(this IdentityType type)
            => "permission";

        public static string UtilityScopePath(this IdentityType type)
            => type == IdentityType.FrontOffice
                ? "foutilityscope"
                : "utilityscope";
    }
}
