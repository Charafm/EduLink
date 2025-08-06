using SchoolSaas.Domain.Common.Enums;


namespace SchoolSaas.Web.IdentityFrontal.Models
{
    public class IdentityHeaderOptions
    {
        public string? ClientId_Backoffice { get; set; }
        public string? ClientId_Frontoffice { get; set; }
        public string grant_type { get; set; }

        public string scope { get; set; }
        public string client_secret { get; set; }
        public  string GetClient(UserType Type)
        {
            var client = "m2m.SchoolSaas.Backoffice";
            if (Type == UserType.Student || Type == UserType.Parent)
            {
                client = "m2m.SchoolSaas.Frontoffice";
                return client;

            }
            return client;
        }
    }
   
}
