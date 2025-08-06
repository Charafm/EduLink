using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Frontoffice.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Frontoffice
{
    public static class FrontofficeDependencyInjection
    {
        public static IServiceCollection AddFrontofficeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
         
           
            //services.AddTransient<IFrontofficeService, FrontofficeService>();
            services.AddTransient<IBackofficeConnectedService, BackofficeConnectedService>();

            //services.AddSingleton<FrontOfficeHub>();
          

            return services;
        }
    }
}
