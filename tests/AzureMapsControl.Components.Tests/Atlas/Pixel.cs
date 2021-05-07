namespace AzureMapsControl.Components.Tests.Atlas
{
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class PixelJsonConverterTests : JsonConverterTests<Pixel>
    {
        public PixelJsonConverterTests(): base(new PixelJsonConverter()) { }

        [Fact]
        public void Should_Read()
        {
            var pixel = new Pixel(1, 2);
            var expectedJson = JsonSerializer.Serialize(pixel);
            var result = Read(expectedJson);
            Assert.Equal(expectedJson, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var pixel = new Pixel(1, 2);
            TestAndAssertWrite(pixel, JsonSerializer.Serialize(pixel));
        }
    }
}
