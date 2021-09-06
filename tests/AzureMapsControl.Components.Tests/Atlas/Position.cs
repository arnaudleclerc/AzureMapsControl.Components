namespace AzureMapsControl.Components.Tests.Atlas
{
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class PositionJsonConverterTests : JsonConverterTests<Position>
    {
        public PositionJsonConverterTests() : base(new PositionJsonConverter()) { }

        [Fact]
        public void Should_Read()
        {
            var position = new Position(1, 2, 3);
            var expectedJson = JsonSerializer.Serialize(position);
            var result = Read(expectedJson);
            Assert.Equal(expectedJson, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var position = new Position(1, 2, 3);
            TestAndAssertWrite(position, JsonSerializer.Serialize(position));
        }
    }
}
