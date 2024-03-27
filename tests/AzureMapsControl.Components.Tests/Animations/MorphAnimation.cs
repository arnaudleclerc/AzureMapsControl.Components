﻿namespace AzureMapsControl.Components.Tests.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class MorphAnimationTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();

        [Fact]
        public async Task Should_SetOptionsAsync()
        {
            var id = "id";
            var animation = new MorphAnimation(id, _jsRuntime.Object);
            var options = new MorphAnimationOptions();
            await animation.SetOptionsAsync(options);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetOptions.ToAnimationNamespace(), id, options), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
