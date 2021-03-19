namespace AzureMapsControl.Components.Tests.Animations
{
    using System;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class FlowingDashedLineAnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();

        [Fact]
        public async void Should_ThrowException_SeekAsync()
        {
            var id = "id";
            var animation = new FlowingDashedLineAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.SeekAsync(1m));

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ThrowException_SetOptionsAsync()
        {
            var id = "id";
            var animation = new FlowingDashedLineAnimation(id, _jsRuntime.Object);
            var options = new MovingDashLineOptions();
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.SetOptionsAsync(options));

            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
