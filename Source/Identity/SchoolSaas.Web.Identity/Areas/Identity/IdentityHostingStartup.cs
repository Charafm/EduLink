using SchoolSaas.Web.Identity.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace SchoolSaas.Web.Identity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
