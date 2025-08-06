using SchoolSaas.Application.Common;
using FluentValidation;
using MediatR;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Application.Backoffice
{
    public static class BackofficeDependencyInjection
    {
        public static IServiceCollection AddBackofficeApplication(this IServiceCollection services)
        {
            services.AddCommonApplication();
         
            var assembly = typeof(BackofficeDependencyInjection).Assembly;

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatorAuthorization(assembly);
            services.AddAuthorizersFromAssembly(assembly);

   

            return services;
        }
    }
}