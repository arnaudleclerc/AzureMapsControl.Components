namespace AzureMapsControl.Components.Tests.Popups
{
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;
    public class HtmlMarkerPopupTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_HaveId_IfInvalidOneIsGiven(string id)
        {
            var popup = new HtmlMarkerPopup(id, null);
            Assert.False(string.IsNullOrWhiteSpace(popup.Id));
        }

        [Fact]
        public void Should_BeInitialized_DefaultId()
        {
            var options = new PopupOptions();
            var popup = new HtmlMarkerPopup(options);
            Assert.NotNull(popup.Id);
            Assert.Equal(options, popup.Options);
        }

        [Fact]
        public void Should_BeInitialized()
        {
            const string id = "id";
            var options = new PopupOptions();
            var popup = new HtmlMarkerPopup(id, options);
            Assert.Equal(id, popup.Id);
            Assert.Equal(options, popup.Options);
        }

        [Fact]
        public async void Should_OpenAsync()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                HasBeenToggled = true
            };
            await popup.OpenAsync();

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Open.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_Not_OpenAsync()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            await popup.OpenAsync();

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_CloseAsync()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                HasBeenToggled = true
            };
            await popup.CloseAsync();
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Close.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_Not_CloseAsync()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            await popup.CloseAsync();
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveAsync()
        {
            var assertRemoveEvent = false;
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                HasBeenToggled = true
            };
            popup.OnRemoved += () => assertRemoveEvent = true;
            await popup.RemoveAsync();

            Assert.True(assertRemoveEvent);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_Not_RemoveAsync()
        {
            var assertRemoveEvent = false;
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            popup.OnRemoved += () => assertRemoveEvent = true;
            await popup.RemoveAsync();

            Assert.False(assertRemoveEvent);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveTwice_Async()
        {
            var assertRemoveEvent = false;
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                HasBeenToggled = true
            };
            popup.OnRemoved += () => assertRemoveEvent = true;
            await popup.RemoveAsync();
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.RemoveAsync());
            Assert.True(assertRemoveEvent);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_Update_Async()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                HasBeenToggled = true
            };
            var updatedContent = "updatedContent";
            await popup.UpdateAsync(options => options.Content = updatedContent);
            Assert.Equal(updatedContent, popup.Options.Content);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Update.ToPopupNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && (parameters[1] as PopupOptions).Content == "updatedContent"
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_Not_Update_Async()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            var updatedContent = "updatedContent";
            await popup.UpdateAsync(options => options.Content = updatedContent);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_DispatchOpenEvent()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                HasBeenToggled = true
            };
            var eventArgs = new PopupEventArgs {
                Id = "id",
                Type = "open"
            };
            var assertEvent = false;
            popup.OnOpen += e => assertEvent = e == eventArgs;
            popup.DispatchEvent(eventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragEvent()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                HasBeenToggled = true
            };
            var eventArgs = new PopupEventArgs {
                Id = "id",
                Type = "drag"
            };
            var assertEvent = false;
            popup.OnDrag += e => assertEvent = e == eventArgs;
            popup.DispatchEvent(eventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragStartEvent()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                HasBeenToggled = true
            };
            var eventArgs = new PopupEventArgs {
                Id = "id",
                Type = "dragstart"
            };
            var assertEvent = false;
            popup.OnDragStart += e => assertEvent = e == eventArgs;
            popup.DispatchEvent(eventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragEndEvent()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                HasBeenToggled = true
            };
            var eventArgs = new PopupEventArgs {
                Id = "id",
                Type = "dragend"
            };
            var assertEvent = false;
            popup.OnDragEnd += e => assertEvent = e == eventArgs;
            popup.DispatchEvent(eventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchCloseEvent()
        {
            var popup = new HtmlMarkerPopup(new PopupOptions()) {
                HasBeenToggled = true
            };
            var eventArgs = new PopupEventArgs {
                Id = "id",
                Type = "close"
            };
            var assertEvent = false;
            popup.OnClose += e => assertEvent = e == eventArgs;
            popup.DispatchEvent(eventArgs);
            Assert.True(assertEvent);
        }
    }
}
