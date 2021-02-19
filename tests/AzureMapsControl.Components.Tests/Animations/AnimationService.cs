namespace AzureMapsControl.Components.Tests.Animations
{
    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class AnimationServiceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();
        private readonly Mock<ILogger<AnimationService>> _loggerServiceMock = new();

        private readonly AnimationService _animationService;

        public AnimationServiceTests() => _animationService = new AnimationService(_jsRuntimeMock.Object, _loggerServiceMock.Object);

        [Fact]
        public async void Should_Snakeline_Async()
        {
            var line = new LineString();
            var source = new DataSource();
            var options = new SnakeLineAnimationOptions();

            var result = await _animationService.SnakelineAsync(line, source, options);
            Assert.NotNull(result.Id);
            Assert.Equal(options, result.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), result.Id, line.Id, source.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
