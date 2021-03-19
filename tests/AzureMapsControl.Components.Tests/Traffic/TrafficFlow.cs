namespace AzureMapsControl.Components.Tests.Traffic
{
    using AzureMapsControl.Components.Traffic;

    using Xunit;

    public class TrafficFlowTests
    {
        [Theory]
        [InlineData("absolute")]
        [InlineData("none")]
        [InlineData("relative")]
        [InlineData("relative-delay")]
        public static void Should_ReturnTrafficFlowFromString(string trafficFlowType)
        {
            var trafficFlow = TrafficFlow.FromString(trafficFlowType);
            Assert.Equal(trafficFlowType, trafficFlow.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultTrafficFlow_IfStringDoesNotMatch()
        {
            var trafficFlowType = "obviouslyNotAValidOne";
            var trafficFlow = TrafficFlow.FromString(trafficFlowType);
            Assert.Equal(default, trafficFlow);
        }
    }
}
