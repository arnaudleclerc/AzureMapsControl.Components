namespace AzureMapsControl.Components.Tests.Markers
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;

    using Xunit;
    public class HtmlMarkerInvokeHelperTests
    {
        [Fact]
        public async Task Should_InvokeCallback_Async()
        {
            var htmlMarkerEventArgs = new HtmlMarkerJsEventArgs();
            var assertEqualEventArgs = false;
            var invokeHelper = new HtmlMarkerInvokeHelper(async (eventArgs) => {
                assertEqualEventArgs = eventArgs == htmlMarkerEventArgs;
            });

            await invokeHelper.NotifyEventAsync(htmlMarkerEventArgs);

            Assert.True(assertEqualEventArgs);
        }
    }
}
