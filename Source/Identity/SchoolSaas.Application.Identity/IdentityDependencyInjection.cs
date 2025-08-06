using SchoolSaas.Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Application.Identity
{
    public static class IdentityDependencyInjection
    {
        public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
        {
            services.AddCommonApplication();

            var assembly = typeof(IdentityDependencyInjection).Assembly;

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
          //services.AddMediatorAuthorization(assembly);
            //services.AddAuthorizersFromAssembly(assembly);

            return services;
        }
    }
}