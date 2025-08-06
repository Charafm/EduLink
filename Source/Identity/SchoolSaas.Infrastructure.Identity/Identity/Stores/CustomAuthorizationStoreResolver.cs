using OpenIddict.Abstractions;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class CustomAuthorizationStoreResolver : IOpenIddictAuthorizationStoreResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Type _defaultStoreType;

        public CustomAuthorizationStoreResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _defaultStoreType = typeof(CustomAuthorizationStore);
        }

        public IOpenIddictAuthorizationStore<TAuthorization> Get<TAuthorization>() where TAuthorization : class
        {
            var storeType = typeof(IOpenIddictAuthorizationStore<>).MakeGenericType(typeof(TAuthorization));
            var customStore = _serviceProvider.GetService(storeType) as IOpenIddictAuthorizationStore<TAuthorization>;

            if (customStore != null)
            {
                return customStore;
            }

            var defaultStore = _serviceProvider.GetService(_defaultStoreType) as IOpenIddictAuthorizationStore<TAuthorization>;

            if (defaultStore != null)
            {
                return defaultStore;
            }

            throw new InvalidOperationException($"No store found for type '{typeof(TAuthorization).Name}'.");
        }
    }
}
