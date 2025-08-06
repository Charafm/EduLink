
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Backoffice.Context;

namespace SchoolSaas.Infrastructure.Backoffice
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedBackofficeDatabase(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BackofficeContext>()!;
                if (context != null)
                {

                    //// add default customerTypes
                    //SeedCustomerTypes(context);

                    //// add default orderTypeCategories & orderTypes
                    //SeedOrderTypeCategories(context);

                    //// add default orderPlans
                    //SeedOrderPlans(context);
                }
            }

            return app;
        }

    }
}
