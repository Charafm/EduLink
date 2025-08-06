using SchoolSaas.Application.Common.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class CustomTokenStoreResolver : IOpenIddictTokenStoreResolver
    {
        private readonly IServiceProvider _provider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomTokenStoreResolver(IServiceProvider provider, IHttpContextAccessor httpContextAccessor)
        {
            _provider = provider;
            _httpContextAccessor = httpContextAccessor;
        }

        public IOpenIddictTokenStore<TToken> Get<TToken>() where TToken : class
        {
            var request = _httpContextAccessor.HttpContext?.GetOpenIddictServerRequest();
            if (request == null)
            {
                throw new InvalidOperationException("OpenIddict request cannot be null.");
            }

            var clientId = request.ClientId;
           
            if (string.IsNullOrEmpty(clientId))
            {
                // Fallback to another mechanism to get client ID
                clientId = _httpContextAccessor.HttpContext?.Items["ClientId"]?.ToString();
                if (string.IsNullOrEmpty(clientId))
                {
                    throw new InvalidOperationException("Client ID not found in the OpenIddict server request.");
                }
            }
            var contextFactory = _provider.GetRequiredService<IContextFactory>();
            var dbContext = contextFactory.CreateContext(clientId);

            return ActivatorUtilities.CreateInstance<CustomTokenStore>(_provider, dbContext) as IOpenIddictTokenStore<TToken>
              ?? throw new InvalidOperationException("The specified token store is not valid.");
        }
    }
}
