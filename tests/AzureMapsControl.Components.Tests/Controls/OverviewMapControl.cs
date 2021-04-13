namespace AzureMapsControl.Components.Tests.Controls
{
    using System;

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class OverviewMapControlTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Fact]
        public void Should_Create()
        {
            var options = new OverviewMapControlOptions();
            var position = ControlPosition.BottomLeft;
            var control = new OverviewMapControl(options, position);
            Assert.Equal(options, control.Options);
            Assert.Equal(position, control.Position);
            Assert.NotEqual(Guid.Empty, control.Id);
            Assert.Equal("overviewmap", control.Type);
        }

        [Fact]
        public async void Should_UpdateAsync()
        {
            var options = new OverviewMapControlOptions();
            var position = ControlPosition.BottomLeft;
            var control = new OverviewMapControl(options, position) {
                JsRuntime = _jsRuntimeMock.Object
            };

            await control.UpdateAsync(options => options.Interactive = true);
            Assert.True(options.Interactive);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateControl.ToCoreNamespace(), control), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotUpdate_NotAddtoMapCase_Async()
        {
            var control = new OverviewMapControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async() => await control.UpdateAsync(options => options.Interactive = true));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateAsyncWithDefaultOptionsAsync()
        {
            var control = new OverviewMapControl
            {
                JsRuntime = _jsRuntimeMock.Object
            };

            await control.UpdateAsync(options => options.Interactive = true);
            Assert.True(control.Options.Interactive);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateControl.ToCoreNamespace(), control), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
