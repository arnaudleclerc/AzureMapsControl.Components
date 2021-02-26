namespace AzureMapsControl.Components.Tests.Animations
{
    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class AnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new();

        [Fact]
        public void Should_Create()
        {
            var id = "id";
            var animation = new Animation(id, _jsRuntime.Object);
            Assert.Equal(id, animation.Id);
        }

        [Fact]
        public async void Should_DisposeAsync()
        {
            var id = "id";
            var animation = new Animation(id, _jsRuntime.Object);
            await animation.DisposeAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Dispose.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_PauseAsync()
        {
            var id = "id";
            var animation = new Animation(id, _jsRuntime.Object);
            await animation.PauseAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Pause.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_PlayAsync()
        {
            var id = "id";
            var animation = new Animation(id, _jsRuntime.Object);
            await animation.PlayAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Play.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ResetAsync()
        {
            var id = "id";
            var animation = new Animation(id, _jsRuntime.Object);
            await animation.ResetAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Reset.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SeekAsync()
        {
            var id = "id";
            var animation = new UpdatableAnimation(id, _jsRuntime.Object);
            var seek = 0.5m;
            await animation.SeekAsync(seek);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Seek.ToAnimationNamespace(), id, seek), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_StopAsync()
        {
            var id = "id";
            var animation = new Animation(id, _jsRuntime.Object);
            await animation.StopAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Stop.ToAnimationNamespace(), id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptionsAsync()
        {
            var id = "id";
            var animation = new UpdatableAnimation(id, _jsRuntime.Object);

            var newOptions = new Mock<IAnimationOptions>();
            await animation.SetOptionsAsync(newOptions.Object);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), id, newOptions.Object), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
