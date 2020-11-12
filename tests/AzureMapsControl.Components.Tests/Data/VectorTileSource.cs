namespace AzureMapsControl.Components.Tests.Data
{
    using AzureMapsControl.Components.Data;

    using Xunit;

    public class VectorTileSourceTests
    {
        [Fact]
        public void Should_BeInitialized()
        {
            var id = "id";
            var source = new VectorTileSource(id);
            Assert.Equal(id, source.Id);
            Assert.Null(source.Options);
        }

        [Fact]
        public void Should_BeInitialized_DefaultId()
        {
            var source = new VectorTileSource();
            Assert.False(string.IsNullOrWhiteSpace(source.Id));
            Assert.Null(source.Options);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_BeInitialized_InvalidId(string id)
        {
            var source = new VectorTileSource(id);
            Assert.False(string.IsNullOrWhiteSpace(source.Id));
            Assert.Null(source.Options);
        }
    }
}
