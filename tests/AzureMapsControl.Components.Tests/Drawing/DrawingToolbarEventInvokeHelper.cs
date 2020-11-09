namespace AzureMapsControl.Components.Tests.Drawing
{
    using AzureMapsControl.Components.Drawing;

    using Xunit;

    public class DrawingToolbarEventInvokeHelperTests
    {
        [Fact]
        public async void Should_InvokeCallback_Async()
        {
            var drawingToolbarEventArgs = new DrawingToolbarJsEventArgs();
            var assertEqualEventArgs = false;
            var invokeHelper = new DrawingToolbarEventInvokeHelper(async (eventArgs) => {
                assertEqualEventArgs = eventArgs == drawingToolbarEventArgs;
            });

            await invokeHelper.NotifyEventAsync(drawingToolbarEventArgs);

            Assert.True(assertEqualEventArgs);
        }
    }
}
