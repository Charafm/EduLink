using SchoolSaas.Application.Common;
using FluentValidation;
using MediatR;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Application.Frontoffice
{
    public static class FrontofficeDependencyInjection
    {
        public static IServiceCollection AddFrontofficeApplication(this IServiceCollection services)
        {
            services.AddCommonApplication();
         
            var assembly = typeof(FrontofficeDependencyInjection).Assembly;

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatorAuthorization(assembly);
            services.AddAuthorizersFromAssembly(assembly);

   

            return services;
        }
    }
}