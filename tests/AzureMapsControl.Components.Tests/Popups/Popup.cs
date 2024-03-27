namespace AzureMapsControl.Components.Tests.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.PortableExecutable;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class PopupTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_HaveId_IfInvalidOneIsGiven(string id)
        {
            var popup = new Popup(id, null);
            Assert.False(string.IsNullOrWhiteSpace(popup.Id));
        }

        [Fact]
        public void Should_BeInitialized_DefaultId()
        {
            var options = new PopupOptions();
            var popup = new Popup(options);
            Assert.NotNull(popup.Id);
            Assert.Equal(options, popup.Options);
        }

        [Fact]
        public void Should_BeInitialized()
        {
            const string id = "id";
            var options = new PopupOptions();
            var popup = new Popup(id, options);
            Assert.Equal(id, popup.Id);
            Assert.Equal(options, popup.Options);
        }

        [Fact]
        public async Task Should_OpenAsync()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            await popup.OpenAsync();

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Open.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotOpen_NotAddedToMapCase_Async()
        {
            var popup = new Popup(new PopupOptions());

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.OpenAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotOpen_RemovedCase_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                IsRemoved = true
            };

            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.OpenAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_CloseAsync()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            await popup.CloseAsync();
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Close.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotClose_NotAddedToMapCase_Async()
        {
            var popup = new Popup(new PopupOptions());
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.CloseAsync());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotClose_RemovedCase_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                IsRemoved = true
            };
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.CloseAsync());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveAsync()
        {
            var assertRemoveEvent = false;
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            popup.OnRemoved += () => assertRemoveEvent = true;
            await popup.RemoveAsync();

            Assert.True(assertRemoveEvent);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemove_NotAddedToMapCase_Async()
        {
            var popup = new Popup(new PopupOptions());
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.RemoveAsync());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveTwice_Async()
        {
            var assertRemoveEvent = false;
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            popup.OnRemoved += () => assertRemoveEvent = true;
            await popup.RemoveAsync();
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.RemoveAsync());
            Assert.True(assertRemoveEvent);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Update_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            var updatedContent = "updatedContent";
            await popup.UpdateAsync(options => options.Content = updatedContent);
            Assert.Equal(updatedContent, popup.Options.Content);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.SetOptions.ToPopupNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && (parameters[1] as PopupOptions).Content == "updatedContent"
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotUpdate_NotAddedToMapCase_Async()
        {
            var popup = new Popup(new PopupOptions());
            var updatedContent = "updatedContent";
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.UpdateAsync(options => options.Content = updatedContent));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotUpdate_RemovedCase_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                IsRemoved = true
            };
            var updatedContent = "updatedContent";
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.UpdateAsync(options => options.Content = updatedContent));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptions_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            var updatedContent = "updatedContent";
            await popup.SetOptionsAsync(options => options.Content = updatedContent);
            Assert.Equal(updatedContent, popup.Options.Content);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.SetOptions.ToPopupNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && (parameters[1] as PopupOptions).Content == "updatedContent"
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_NotAddedToMapCase_Async()
        {
            var popup = new Popup(new PopupOptions());
            var updatedContent = "updatedContent";
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.SetOptionsAsync(options => options.Content = updatedContent));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_RemovedCase_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object,
                IsRemoved = true
            };
            var updatedContent = "updatedContent";
            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.SetOptionsAsync(options => options.Content = updatedContent));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_DispatchOpenEvent()
        {
            var popup = new Popup(new PopupOptions());
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
            var popup = new Popup(new PopupOptions());
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
            var popup = new Popup(new PopupOptions());
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
            var popup = new Popup(new PopupOptions());
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
            var popup = new Popup(new PopupOptions());
            var eventArgs = new PopupEventArgs {
                Id = "id",
                Type = "close"
            };
            var assertEvent = false;
            popup.OnClose += e => assertEvent = e == eventArgs;
            popup.DispatchEvent(eventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public async Task Should_ApplyTemplateAsync()
        {
            var popup = new Popup() {
                JSRuntime = _jsRuntimeMock.Object
            };

            var template = new PopupTemplate();
            var properties = new Dictionary<string, object>();

            await popup.ApplyTemplateAsync(template, properties);

            Assert.NotNull(popup.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.ApplyTemplate.ToPopupNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && parameters[1] as PopupOptions == popup.Options
                && parameters[2] as Dictionary<string, object> == properties
                && parameters[3] as PopupTemplate == template
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotApplyTemplate_NotAddedToMapCaseAsync()
        {
            var popup = new Popup();

            var template = new PopupTemplate();
            var properties = new Dictionary<string, object>();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.ApplyTemplateAsync(template, properties));

            Assert.Null(popup.Options);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotApplyTemplate_RemovedCaseAsync()
        {
            var popup = new Popup() {
                JSRuntime = _jsRuntimeMock.Object,
                IsRemoved = true
            };
            var template = new PopupTemplate();
            var properties = new Dictionary<string, object>();

            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.ApplyTemplateAsync(template, properties));

            Assert.Null(popup.Options);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotApplyTemplateWithNullPropertiesAsync()
        {
            var popup = new Popup() {
                JSRuntime = _jsRuntimeMock.Object
            };

            var template = new PopupTemplate();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await popup.ApplyTemplateAsync(template, null));

            Assert.Null(popup.Options);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ApplyTemplateWithOptionsAsync()
        {
            var popup = new Popup() {
                JSRuntime = _jsRuntimeMock.Object
            };

            var template = new PopupTemplate();
            var properties = new Dictionary<string, object>();

            await popup.ApplyTemplateAsync(template, properties, options => options.FillColor = "fillColor");

            Assert.Equal("fillColor", popup.Options.FillColor);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.ApplyTemplate.ToPopupNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && parameters[1] as PopupOptions == popup.Options
                && parameters[2] as Dictionary<string, object> == properties
                && parameters[3] as PopupTemplate == template
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotApplyTemplateWithOptions_NotAddedToMapCaseAsync()
        {
            var popup = new Popup();

            var template = new PopupTemplate();
            var properties = new Dictionary<string, object>();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await popup.ApplyTemplateAsync(template, properties, options => options.FillColor = "fillColor"));

            Assert.Null(popup.Options);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotApplyTemplateWithOptions_RemovedCaseAsync()
        {
            var popup = new Popup() {
                JSRuntime = _jsRuntimeMock.Object,
                IsRemoved = true
            };
            var template = new PopupTemplate();
            var properties = new Dictionary<string, object>();

            await Assert.ThrowsAnyAsync<PopupAlreadyRemovedException>(async () => await popup.ApplyTemplateAsync(template, properties, options => options.FillColor = "fillColor"));

            Assert.Null(popup.Options);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotApplyTemplateWithOptionsWithNullPropertiesAsync()
        {
            var popup = new Popup() {
                JSRuntime = _jsRuntimeMock.Object
            };

            var template = new PopupTemplate();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await popup.ApplyTemplateAsync(template, null, options => options.FillColor = "fillColor"));

            Assert.Null(popup.Options);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
