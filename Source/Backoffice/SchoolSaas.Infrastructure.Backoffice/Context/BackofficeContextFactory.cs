using SchoolSaas.Infrastructure.Common.Context;


namespace SchoolSaas.Infrastructure.Backoffice.Context
{
    public class BackofficeContextFactory : AbstractContextFactory<BackofficeContext>
    {
        public BackofficeContextFactory()
        {
        }

        protected override string GetAppSettingsProject()
        {
            return "../SchoolSaas.Web.Backoffice";
        }
    }
}