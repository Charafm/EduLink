using SchoolSaas.Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SchoolSaas.Infrastructure.Common.Context
{
    public abstract class AbstractContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public virtual TContext CreateDbContext(string[] args)
        {
            return Activator.CreateInstance(typeof(TContext), GetParams()) as TContext
                ?? throw new InvalidOperationException("Failed to create DbContext instance");
        }

        protected virtual DbContextOptions<TContext> BuildDbContextOptions()
        {
            var configurationRoot = GetConfigurationRoot();
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString(typeof(TContext).Name),
                b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(TContext))?.GetName().FullName));

            return optionsBuilder.Options;
        }


        protected virtual object?[] GetParams()
        {
            return new object?[]
            {
                new NullTenantAccessor(),
                new NullCurrentUserService(),
                new NullDateTimeService(),
                new NullStorageService(),
                new NullCacheService(),
                BuildDbContextOptions()
            };
        }


        protected IConfigurationRoot GetConfigurationRoot()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), GetAppSettingsProject()))
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }

        protected abstract string GetAppSettingsProject();
    }
}