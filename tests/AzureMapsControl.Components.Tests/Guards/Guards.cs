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
    }
}
