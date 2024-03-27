namespace AzureMapsControl.Components.Tests.Animations
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class MoveAlongRouteAnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();

        [Fact]
        public async Task Should_ThrowException_PauseAsync()
        {
            var id = "id";
            var animation = new MoveAlongRouteAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.PauseAsync());

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowException_PlayAsync()
        {
            var id = "id";
            var animation = new MoveAlongRouteAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.PlayAsync());

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowException_ResetAsync()
        {
            var id = "id";
            var animation = new MoveAlongRouteAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.ResetAsync());

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowException_SeekAsync()
        {
            var id = "id";
            var animation = new MoveAlongRouteAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.SeekAsync(1m));

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowException_StopAsync()
        {
            var id = "id";
            var animation = new MoveAlongRouteAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.StopAsync());

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowException_SetOptionsAsync()
        {
            var id = "id";
            var animation = new MoveAlongRouteAnimation(id, _jsRuntime.Object);
            var options = new RoutePathAnimationOptions();
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.SetOptionsAsync(options));

            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
