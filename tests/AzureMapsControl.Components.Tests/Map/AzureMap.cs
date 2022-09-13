namespace AzureMapsControl.Components.Tests.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;

    using AzureMapsControl.Components.Configuration;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Runtime;

    using Bunit;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class AzureMapFixture : IDisposable
    {
        internal Mock<IMapAdderService> MapService { get; }
        internal Mock<IMapJsRuntime> JsRuntime { get; }
        public Mock<ILogger<AzureMap>> Logger { get; }
        public AzureMapsConfiguration Configuration { get; }

        public TestContext TestContext { get; }

        public AzureMapFixture()
        {
            MapService = new Mock<IMapAdderService>();
            JsRuntime = new Mock<IMapJsRuntime>();
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
        [Fact]
        public void Should_DisplayDivWithId()
        {
            using (var fixture = new AzureMapFixture())
            {
                const string mapId = "mapID";

                fixture.Configuration.AadAppId = "aadAppId";

                fixture.Configuration.AadTenant = "aadTenant";
                fixture.Configuration.ClientId = "clientId";
                fixture.Configuration.SubscriptionKey = "subscriptionKey";

                fixture.RegisterServices();

                var map = fixture.TestContext.RenderComponent<AzureMap>(ComponentParameter.CreateParameter("Id", mapId));

                var mapElem = map.Find("div");

                Assert.Equal(mapId, mapElem.Attributes["id"].Value);

                fixture.JsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddMap.ToCoreNamespace(), It.Is<object[]>(parameters =>
                    parameters.Length == 5
                    && parameters[0] as string == mapId
                    && (parameters[1] as AzureMapsConfiguration).AadAppId == fixture.Configuration.AadAppId
                    && (parameters[1] as AzureMapsConfiguration).AadTenant == fixture.Configuration.AadTenant
                    && (parameters[1] as AzureMapsConfiguration).ClientId == fixture.Configuration.ClientId
                    && (parameters[1] as AzureMapsConfiguration).SubscriptionKey == fixture.Configuration.SubscriptionKey
                    && parameters[2] != null && parameters[2] is ServiceOptions
                    && (parameters[3] as IEnumerable<string>).Single() == MapEventType.Ready.ToString()
                    && parameters[4] != null && parameters[4] is DotNetObjectReference<MapEventInvokeHelper>
                )), Times.Once);
                fixture.JsRuntime.VerifyNoOtherCalls();
            }
        }
        
        [Fact]
        public void Should_DisplayDivWithClass()
        {
            using (var fixture = new AzureMapFixture())
            {
                const string classes = "my-class-identifier other-class-identifier";
                const string mapId = "mapID";

                fixture.Configuration.AadAppId = "aadAppId";

                fixture.Configuration.AadTenant = "aadTenant";
                fixture.Configuration.ClientId = "clientId";
                fixture.Configuration.SubscriptionKey = "subscriptionKey";

                fixture.RegisterServices();

                var map = fixture.TestContext.RenderComponent<AzureMap>(ComponentParameter.CreateParameter("Id", mapId), ComponentParameter.CreateParameter("Class", classes));

                var mapElem = map.Find("div");

                Assert.Equal(classes, mapElem.Attributes["class"].Value);

                fixture.JsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddMap.ToCoreNamespace(), It.Is<object[]>(parameters =>
                    parameters.Length == 5
                    && parameters[0] as string == mapId
                    && (parameters[1] as AzureMapsConfiguration).AadAppId == fixture.Configuration.AadAppId
                    && (parameters[1] as AzureMapsConfiguration).AadTenant == fixture.Configuration.AadTenant
                    && (parameters[1] as AzureMapsConfiguration).ClientId == fixture.Configuration.ClientId
                    && (parameters[1] as AzureMapsConfiguration).SubscriptionKey == fixture.Configuration.SubscriptionKey
                    && parameters[2] != null && parameters[2] is ServiceOptions
                    && (parameters[3] as IEnumerable<string>).Single() == MapEventType.Ready.ToString()
                    && parameters[4] != null && parameters[4] is DotNetObjectReference<MapEventInvokeHelper>
                )), Times.Once);
                fixture.JsRuntime.VerifyNoOtherCalls();
            }
        }
    }
}
