namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class LayerEventInvokeHelperTests
    {
        [Fact]
        public async Task Should_InvokeCallback_Async()
        {
            var layerEventArgs = new MapJsEventArgs();
            var assertEqualEventArgs = false;
            var invokeHelper = new LayerEventInvokeHelper(async (eventArgs) => {
                assertEqualEventArgs = eventArgs == layerEventArgs;
            });

            await invokeHelper.NotifyEventAsync(layerEventArgs);

            Assert.True(assertEqualEventArgs);
        }
    }
}
