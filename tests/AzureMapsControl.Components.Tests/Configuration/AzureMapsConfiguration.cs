namespace AzureMapsControl.Components.Tests.Configuration
{
    using AzureMapsControl.Components.Configuration;
    using AzureMapsControl.Components.Tests.Json;

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

    public class AzureMapsConfigurationJsonConverterTests : JsonConverterTests<AzureMapsConfiguration>
    {
        public AzureMapsConfigurationJsonConverterTests() : base(new AzureMapsConfigurationJsonConverter())
        {
        }

        [Fact]
        public void Should_Write_WithSubscriptionKey()
        {
            var configuration = new AzureMapsConfiguration {
                SubscriptionKey = "subKey"
            };
            var expectedJson = "{"
                + "\"authType\":\"" + configuration.AuthType + "\","
                + "\"subscriptionKey\":\"" + configuration.SubscriptionKey + "\""
                + "}";

            TestAndAssertWrite(configuration, expectedJson);
        }

        [Fact]
        public void Should_Write_WithAad()
        {
            var configuration = new AzureMapsConfiguration {
                AadAppId = "appId",
                AadTenant = "tenant",
                ClientId = "client"
            };

            var expectedJson = "{"
                + "\"authType\":\"" + configuration.AuthType + "\","
                + "\"aadAppId\":\"" + configuration.AadAppId + "\","
                + "\"aadTenant\":\"" + configuration.AadTenant + "\","
                + "\"clientId\":\"" + configuration.ClientId + "\""
                + "}";

            TestAndAssertWrite(configuration, expectedJson);
        }

        [Fact]
        public void Should_Write_WithAnonymous()
        {
            var configuration = new AzureMapsConfiguration {
                ClientId = "client"
            };

            var expectedJson = "{"
                + "\"authType\":\"" + configuration.AuthType + "\","
                + "\"clientId\":\"" + configuration.ClientId + "\""
                + "}";

            TestAndAssertWrite(configuration, expectedJson);
        }
    }
}
