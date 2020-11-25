namespace AzureMapsControl.Components.Tests.Map
{
    using System;

    using AzureMapsControl.Components.Configuration;
    using AzureMapsControl.Components.Map;

    using Bunit;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class AzureMapFixture : IDisposable
    {
        internal Mock<IMapAdderService> MapService { get; }
        public Mock<IJSRuntime> JsRuntime { get; }
        public Mock<ILogger<AzureMap>> Logger { get; }
        public AzureMapsConfiguration Configuration { get; }

        public TestContext TestContext { get; }

        public AzureMapFixture()
        {
            MapService = new Mock<IMapAdderService>();
            JsRuntime = new Mock<IJSRuntime>();
            Logger = new Mock<ILogger<AzureMap>>();

            Configuration = new AzureMapsConfiguration();
            TestContext = new TestContext();
        }

        public void RegisterServices()
        {
            TestContext.Services
                .AddSingleton(MapService.Object)
                .AddSingleton(JsRuntime.Object)
                .AddSingleton(Logger.Object)
                .AddOptions<AzureMapsConfiguration>().Configure(options => {
                    options.AadAppId = Configuration.AadAppId;
                    options.AadTenant = Configuration.AadTenant;
                    options.ClientId = Configuration.ClientId;
                    options.SubscriptionKey = Configuration.SubscriptionKey;
                });
        }

        public void Dispose() => TestContext.Dispose();
    }

    public class AzureMapTests : IClassFixture<AzureMapFixture>
    {
        private readonly AzureMapFixture _fixture;
        public AzureMapTests(AzureMapFixture fixture) => _fixture = fixture;

        [Fact]
        public void Should_DisplayDivWithId()
        {
            const string mapId = "mapID";

            _fixture.Configuration.AadAppId = "aadAppId";
            _fixture.Configuration.AadTenant = "aadTenant";
            _fixture.Configuration.ClientId = "clientId";
            _fixture.Configuration.SubscriptionKey = "subscriptionKey";
            _fixture.RegisterServices();

            var map = _fixture.TestContext.RenderComponent<AzureMap>(ComponentParameter.CreateParameter("Id", mapId));

            var mapElem = map.Find("div");

            Assert.Equal(mapId, mapElem.Attributes["id"].Value);
        }
    }
}
