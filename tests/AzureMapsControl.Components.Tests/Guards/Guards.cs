namespace AzureMapsControl.Components.Tests.Guards
{
    using System;

    using AzureMapsControl.Components.Guards;

    using Xunit;

    public class GuardsTests
    {
        [Fact]
        public void NullGuard_Should_ThrowArgumentNullExceptionIfNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => Require.NotNull(null, "name"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NullOrWhitespaceGuard_Should_ThrowArgumentExceptionIfNullOrWhitespace(string value)
        {
            Assert.ThrowsAny<ArgumentException>(() => Require.NotNullOrWhiteSpace(value, "name"));
        }
    }
}
