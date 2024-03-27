namespace AzureMapsControl.Components.Tests.Map
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapEventInvokeHelperTests
    {
        [Fact]
        public async Task Should_InvokeCallback_Async()
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
