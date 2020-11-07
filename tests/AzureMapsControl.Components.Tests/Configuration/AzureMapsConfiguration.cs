namespace AzureMapsControl.Components.Tests.Configuration
{
    using AzureMapsControl.Components.Configuration;

    using Xunit;

    public class AzureMapsConfigurationTests
    {
        [Fact]
        public void Should_HaveSubscriptionKeyAuthType()
        {
            var configuration = new AzureMapsConfiguration {
                AadAppId = "aadAppId",
                AadTenant = "aadTenant",
                ClientId = "clientId",
                SubscriptionKey = "subscriptionKey"
            };

            Assert.Equal("subscriptionKey", configuration.AuthType);
        }

        [Fact]
        public void Should_HaveAddAuthType()
        {
            var configuration = new AzureMapsConfiguration {
                AadAppId = "aadAppId",
                AadTenant = "aadTenant",
                ClientId = "clientId",
            };

            Assert.Equal("aad", configuration.AuthType);
        }

        [Fact]
        public void Should_HaveAnonymousAuthType()
        {
            var configuration = new AzureMapsConfiguration();
            Assert.Equal("anonymous", configuration.AuthType);
        }
    }
}
