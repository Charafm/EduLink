using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Tests.Utilities;
using Xunit.Abstractions;

namespace SchoolSaas.Tests.Tests
{
    public class DynamicSwitchingTests : TestLoggerBase
    {
        public DynamicSwitchingTests(ITestOutputHelper output) : base(output) { }

        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice", typeof(IdentityContext))]
        [InlineData("m2m.SchoolSaas.Frontoffice", typeof(FrontOfficeIdentityContext))]
        public void CreateContext_ShouldReturnCorrectContext(string clientId, Type expectedContextType)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();

            var context = contextFactory.CreateContext(clientId);
            Assert.IsType(expectedContextType, context);
            Log($"✅ {clientId} resolved to {context.GetType().Name}");
        }
    }
}

