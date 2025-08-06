using SchoolSaas.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SchoolSaas.Infrastructure.Identity.Context
{
    public class ContextFactory : IContextFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantAccessor _tenantAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTimeService;

        public ContextFactory(IConfiguration configuration, ITenantAccessor tenantAccessor, ICurrentUserService currentUserService, IDateTime dateTimeService)
        {
            _configuration = configuration;
            _tenantAccessor = tenantAccessor;
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbContext CreateContext(string clientId)
        {
            var contextType = ResolveContextType(clientId);
            if (contextType == typeof(FrontOfficeIdentityContext))
            {
                return new FrontOfficeIdentityContext(_tenantAccessor, _currentUserService, _dateTimeService, BuildDbContextOptions<FrontOfficeIdentityContext>());
            }
            else
            {
                return new IdentityContext(_tenantAccessor, _currentUserService, _dateTimeService, BuildDbContextOptions<IdentityContext>());
            }
        }

        public Type ResolveContextType(string clientId)
        {
            if (ClientContextMapping.ClientContextMap.TryGetValue(clientId, out var contextName))
            {
                if (contextName == "FrontOfficeIdentityContext")
                {
                    return typeof(FrontOfficeIdentityContext);
                }
                else
                {
                    return typeof(IdentityContext);
                }
            }
            throw new InvalidOperationException($"No context mapping found for client ID {clientId}");
        }

        private DbContextOptions<TContext> BuildDbContextOptions<TContext>() where TContext : DbContext
        {
            var configurationRoot = GetConfigurationRoot();
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString(typeof(TContext).Name),
                b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(TContext)).FullName));
            optionsBuilder.UseOpenIddict();

            return optionsBuilder.Options;
        }

        private IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }

}
