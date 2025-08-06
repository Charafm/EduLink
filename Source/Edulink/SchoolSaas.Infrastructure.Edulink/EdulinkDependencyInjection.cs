using SchoolSaas.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Common;
using SchoolSaas.Infrastructure.Edulink.Services;
using SchoolSaas.Infrastructure.Edulink.Context;

namespace SchoolSaas.Infrastructure.Edulink
{
    public static class EdulinkDependencyInjection
    {
        public static IServiceCollection AddEdulinkInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext<EdulinkContext, IEdulinkContext>(configuration);
            services.AddContext<EdulinkReadOnlyContext, IEdulinkReadOnlyContext>(configuration);
            services.AddScoped<IEdulinkContext>(provider => provider.GetService<EdulinkContext>()!);
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddTransient<IReferentialService, ReferentialService>();

            //services.AddSingleton<BackOfficeHub>();
            //services.AddSingleton<FrontOfficeHub>();


            return services;
        }
    }
}