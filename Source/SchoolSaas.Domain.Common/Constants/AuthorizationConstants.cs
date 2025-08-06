namespace SchoolSaas.Domain.Common.Constants
{
    public static class AuthorizationConstants
    {
        public const string DefaultPassword = "EduLink@2025";
        public const string DefaultBackofficeId = "EduLink@2025";

        public static class ClaimTypes
        {
            public const string Permissions = "permissions";
        }

        public static class Scopes
        {
            public const string EduLink = "EduLink";
            public const string BackOffice = "BackOffice";
            public const string Offline_Access = "offline_access";
        }

        public static class Roles
        {
            public const string SuperAdmins = "SuperAdmins";
            public const string Administration = "Administration";
            public const string Teacher = "Teacher";
            public const string Staff = "Staff";
            public const string Parent = "Parent";
            public const string Student = "Student";

        }
    }

}