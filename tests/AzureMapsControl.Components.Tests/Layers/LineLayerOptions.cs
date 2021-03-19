namespace AzureMapsControl.Components.Tests.Layers
{
    using AzureMapsControl.Components.Layers;

    using Xunit;

    public class LineJoinTests
    {
        [Theory]
        [InlineData("bevel")]
        [InlineData("miter")]
        [InlineData("round")]
        public static void Should_ReturnLineJoinFromString(string lineJoinType)
        {
            var lineJoin = LineJoin.FromString(lineJoinType);
            Assert.Equal(lineJoinType, lineJoin.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultLineJoin_IfStringDoesNotMatch()
        {
            var lineJoinType = "obviouslyNotAValidOne";
            var lineJoin = LineJoin.FromString(lineJoinType);
            Assert.Equal(default, lineJoin);
        }
    }
}
