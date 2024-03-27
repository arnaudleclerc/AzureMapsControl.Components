namespace AzureMapsControl.Components.Tests.Drawing
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Drawing;

    using Xunit;

    public class DrawingToolbarEventInvokeHelperTests
    {
        [Fact]
        public async Task Should_InvokeCallback_Async()
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
