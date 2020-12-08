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
            Assert.True(configuration.Validate());
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
            Assert.True(configuration.Validate());
        }

        [Fact]
        public void Should_HaveAnonymousAuthType()
        {
            var configuration = new AzureMapsConfiguration {
                ClientId = "clientId"
            };
            Assert.Equal("anonymous", configuration.AuthType);
            Assert.True(configuration.Validate());
        }

        [Fact]
        public void Should_NotHaveAnAuthType()
        {
            var configuration = new AzureMapsConfiguration();
            Assert.Null(configuration.AuthType);
            Assert.False(configuration.Validate());
        }
    }
}
