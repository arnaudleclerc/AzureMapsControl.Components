
using System.Threading.Tasks;

using AzureMapsControl.Components.Map;

using Xunit;

namespace AzureMapsControl.Components.Tests.Map;
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
