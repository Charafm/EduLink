using SchoolSaas.Infrastructure.Common.Context;
using SchoolSaas.Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SchoolSaas.Infrastructure.Identity.Context
{
    public class FrontOfficeIdentityContextFactory : FrontOfficeIdentityContextFactory<FrontOfficeIdentityContext>
    {
        public FrontOfficeIdentityContextFactory()
        {
        }
    }

    public class FrontOfficeIdentityContextFactory<TContext> : AbstractContextFactory<TContext>
        where TContext : DbContext
    {
        public FrontOfficeIdentityContextFactory()
        {
        }

        protected override DbContextOptions<TContext> BuildDbContextOptions()
        {
            var configurationRoot = GetConfigurationRoot();
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString(typeof(TContext).Name),
                b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(TContext))?.GetName().FullName));
            optionsBuilder.UseOpenIddict();

            return optionsBuilder.Options;
        }

        protected override object?[] GetParams()
        {
            return new object?[]
            {
                new NullTenantAccessor(),
                new NullCurrentUserService(),
                new NullDateTimeService(),
                BuildDbContextOptions(),
            };
        }

        protected override string GetAppSettingsProject()
        {
            return "../SchoolSaas.Web.Identity";
        }
    }
}