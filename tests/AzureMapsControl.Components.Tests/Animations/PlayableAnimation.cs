namespace AzureMapsControl.Components.Tests.Animations
{
    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class PlayableAnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new();

        [Fact]
        public void Should_Create()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            Assert.Equal(id, animation.Id);
            Assert.Equal(options.Object, animation.Options);
        }

        [Fact]
        public async void Should_DisposeAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            await animation.DisposeAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_PauseAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            await animation.PauseAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_PlayAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            await animation.PlayAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ResetAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            await animation.ResetAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SeekAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            var seek = 0.5m;
            await animation.SeekAsync(seek);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), id, seek), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_StopAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);
            await animation.StopAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptionsAsync()
        {
            var id = "id";
            var options = new Mock<IPlayableAnimationOptions>();
            var animation = new PlayableAnimation(id, options.Object, _jsRuntime.Object);

            var newOptions = new Mock<IPlayableAnimationOptions>();
            await animation.SetOptionsAsync(newOptions.Object);
            Assert.Equal(animation.Options, newOptions.Object);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), id, newOptions.Object), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
