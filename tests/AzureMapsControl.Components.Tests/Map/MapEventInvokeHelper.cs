namespace AzureMapsControl.Components.Tests.Map
{
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapEventInvokeHelperTests
    {
        [Fact]
        public async void Should_InvokeCallback_Async()
        {
            var mapEventArgs = new MapJsEventArgs();
            var assertEqualEventArgs = false;
            var invokeHelper = new MapEventInvokeHelper(async (eventArgs) => {
                assertEqualEventArgs = eventArgs == mapEventArgs;
            });

            await invokeHelper.NotifyEventAsync(mapEventArgs);

            Assert.True(assertEqualEventArgs);
        }
    }
}
