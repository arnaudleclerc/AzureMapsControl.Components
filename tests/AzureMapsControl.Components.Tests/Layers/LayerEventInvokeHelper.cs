namespace AzureMapsControl.Components.Tests.Layers
{
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class LayerEventInvokeHelperTests
    {
        [Fact]
        public async void Should_InvokeCallback_Async()
        {
            var drawingToolbarEventArgs = new MapJsEventArgs();
            var assertEqualEventArgs = false;
            var invokeHelper = new LayerEventInvokeHelper(async (eventArgs) => {
                assertEqualEventArgs = eventArgs == drawingToolbarEventArgs;
            });

            await invokeHelper.NotifyEventAsync(drawingToolbarEventArgs);

            Assert.True(assertEqualEventArgs);
        }
    }
}
