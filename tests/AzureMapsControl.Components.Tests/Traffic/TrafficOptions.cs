namespace AzureMapsControl.Components.Tests.Traffic
{
    using AzureMapsControl.Components.Tests.Json;
    using AzureMapsControl.Components.Traffic;

    using Xunit;

    public class TrafficOptionsJsonConverterTests : JsonConverterTests<TrafficOptions>
    {
        public TrafficOptionsJsonConverterTests() : base(new TrafficOptionsJsonConverter()) { }

        [Fact]
        public void Should_ReadTrafficFlow()
        {
            var json = "{"
                + "\"flow\":\"" + TrafficFlow.Relative.ToString() + "\""
                + "}";
            var result = Read(json);
            Assert.Equal(TrafficFlow.Relative, result.Flow);
            Assert.Null(result.Incidents);
        }

        [Fact]
        public void Should_ReadIncidents()
        {
            var json = "{"
                + "\"incidents\":true"
                + "}";
            var result = Read(json);
            Assert.True(result.Incidents);
        }

        [Fact]
        public void Should_ReadTrafficOptions()
        {
            var json = "{"
                + "\"flow\":\"" + TrafficFlow.Relative.ToString() + "\","
                + "\"incidents\":true"
                + "}";
            var result = Read(json);
            Assert.Equal(TrafficFlow.Relative, result.Flow);
            Assert.True(result.Incidents);
        }

        [Fact]
        public async void Should_WriteTrafficOptions()
        {
            var trafficOptions = new TrafficOptions {
                Flow = TrafficFlow.Relative
            };

            var expectedJson = "{"
                + "\"flow\":\"" + trafficOptions.Flow.ToString() + "\""
                + "}";

            TestAndAssertWrite(trafficOptions, expectedJson);
        }

        [Fact]
        public async void Should_WriteTrafficOptions_WithIncidents()
        {
            var trafficOptions = new TrafficOptions {
                Flow = TrafficFlow.Relative,
                Incidents = true
            };

            var expectedJson = "{"
                + "\"flow\":\"" + trafficOptions.Flow.ToString() + "\","
                + "\"incidents\":true"
                + "}";

            TestAndAssertWrite(trafficOptions, expectedJson);
        }
    }
}
