using SchoolSaas.Infrastructure.Common.Context;


namespace SchoolSaas.Infrastructure.Edulink.Context
{
    public class EdulinkContextFactory : AbstractContextFactory<EdulinkContext>
    {
        public EdulinkContextFactory()
        {
        }

        protected override string GetAppSettingsProject()
        {
            return "../SchoolSaas.Web.Edulink";
        }
    }
}