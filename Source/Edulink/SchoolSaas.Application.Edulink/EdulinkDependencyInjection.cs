using SchoolSaas.Application.Common;
using FluentValidation;
using MediatR;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Application.Edulink
{
    public static class EdulinkDependencyInjection
    {
        public static IServiceCollection AddEdulinkApplication(this IServiceCollection services)
        {
            services.AddCommonApplication();
         
            var assembly = typeof(EdulinkDependencyInjection).Assembly;

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatorAuthorization(assembly);
            services.AddAuthorizersFromAssembly(assembly);

   

            return services;
        }
    }
}