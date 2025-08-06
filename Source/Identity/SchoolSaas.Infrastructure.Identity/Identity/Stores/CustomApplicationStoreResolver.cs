using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using Microsoft.AspNetCore.Http;
using SchoolSaas.Infrastructure.Identity.Context;
using Microsoft.AspNetCore;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class CustomApplicationStoreResolver : IOpenIddictApplicationStoreResolver
    {
        private readonly IServiceProvider _provider;

        public CustomApplicationStoreResolver(IServiceProvider provider)
        {
            _provider = provider;
        }

       public IOpenIddictApplicationStore<TApplication> Get<TApplication>() where TApplication : class
{
    var request = _provider.GetRequiredService<IHttpContextAccessor>().HttpContext?.GetOpenIddictServerRequest();
    
    if (request == null || string.IsNullOrEmpty(request.ClientId))
    {
        throw new InvalidOperationException("Client ID is missing from OpenIddict request.");
    }

    var clientId = request.ClientId;

            object dbContext;
            if (ClientContextMapping.ClientContextMap.ContainsKey(clientId))
            {
                dbContext = _provider.GetRequiredService<FrontOfficeIdentityContext>();
            }
            else
            {
                dbContext = _provider.GetRequiredService<IdentityContext>();
            }

            return ActivatorUtilities.CreateInstance<CustomApplicationStore>(_provider, dbContext) as IOpenIddictApplicationStore<TApplication>
        ?? throw new InvalidOperationException("The specified application store is not valid.");
}

    }
}
