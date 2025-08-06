namespace SchoolSaas.Domain.Common.Constants
{
    public static class TemplatesNames
    {
        public static class PDFs
        {
            public const string FunctionSheet = "Application/FunctionSheet";
            public const string FunctionSheetRecapDaily = "Application/FunctionSheetRecapDaily";
            public const string FunctionSheetRecapWeekly = "Application/FunctionSheetRecapWeekly";
            public const string TravelPassTemplate = "TravelPass";
            public const string RegistrationCertificate = "RegistrationCertificate";
            public const string RegistrationCertificateForCustoms = "RegistrationCertificateForCustoms";
            public const string RegistrationCard = "RegistrationCard";
            public const string RegistrationSheet = "RegistrationSheet";
            public const string RegistrationRequestForm = "RegistrationRequestForm";
            public const string RepatriationRequestAutorisation = "RepatriationRequestAutorisation";
        }
        public static class Emails
        {
            public const string Commissions = "Application/Commissions";
            public const string Register = "Account/Register";
            public const string ResetPassword = "Account/ResetPassword";
            public const string CINEmail = "Application/CINEmail";
            public const string VerifyRepatriation = "Repatriation/VerifyRepatriation";
            public const string CreatedRequest = "Repatriation/CreatedRequest";
            public const string ValidateRegistrationRequest = "Registration/RequestValidationEmail";
            public const string PasswordForgotten = "Account/ForgotPassword";

        }
    }
}