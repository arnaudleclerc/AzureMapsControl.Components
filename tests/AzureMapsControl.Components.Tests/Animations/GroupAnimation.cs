namespace AzureMapsControl.Components.Tests.Animations
{
    using System;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class GroupAnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();

        [Fact]
        public async void Should_ThrowException_SeekAsync()
        {
            var id = "id";
            var animation = new GroupAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.SeekAsync(1m));

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ThrowException_PauseAsync()
        {
            var id = "id";
            var animation = new GroupAnimation(id, _jsRuntime.Object);
            await Assert.ThrowsAnyAsync<NotSupportedException>(async () => await animation.PauseAsync());

            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptionsAsync()
        {
            var id = "id";
            var animation = new GroupAnimation(id, _jsRuntime.Object);
            var options = new GroupAnimationOptions();
            await animation.SetOptionsAsync(options);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), id, options), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
