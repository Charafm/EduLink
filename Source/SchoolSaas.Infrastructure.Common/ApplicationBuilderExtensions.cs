using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Infrastructure.Common
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder RunDbMigrations<TContext>(this WebApplication app)
            where TContext : DbContext
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TContext>()!;
                if (context != null)
                    context.Database.Migrate();
            }


            return app;
        }
    }
}
