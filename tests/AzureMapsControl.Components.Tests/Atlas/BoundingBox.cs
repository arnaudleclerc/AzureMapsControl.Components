namespace AzureMapsControl.Components.Tests.Atlas
{

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class BoundingBoxJsonConverterTests : JsonConverterTests<BoundingBox>
    {
        public BoundingBoxJsonConverterTests() : base(new BoundingBoxJsonConverter())
        {
        }

        [Fact]
        public void Should_Write()
        {
            var boundingBox = new BoundingBox(1, 2, 3, 4);
            var expectedJson = "[1,2,3,4]";

            TestAndAssertWrite(boundingBox, expectedJson);
        }

        [Fact]
        public void Should_Read()
        {
            var json = "[1,2,3,4]";

            var boundingBox = Read(json);
            Assert.Equal(1, boundingBox.West);
            Assert.Equal(2, boundingBox.South);
            Assert.Equal(3, boundingBox.East);
            Assert.Equal(4, boundingBox.North);
        }
    }
}
