namespace AzureMapsControl.Components.Tests.Markers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class HtmlMarkerTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();

        [Fact]
        public void Should_HaveDefaultIdAndDeactivatedEvents()
        {
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            Assert.False(string.IsNullOrWhiteSpace(marker.Id));
            Assert.Empty(marker.EventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_UpdateOptionsWithEvent()
        {
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var options = new HtmlMarkerOptions {
                Color = "#bbbbbb"
            };

            var map = new Components.Map.Map("id");
            marker.DispatchEvent(new Components.Map.Map("id"), new HtmlMarkerJsEventArgs { Options = options });
            Assert.Equal(options, marker.Options);
        }

        [Fact]
        public void Should_NotUpdateOptionsWithEvent()
        {
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var options = new HtmlMarkerOptions {
                Color = "#bbbbbb"
            };

            var map = new Components.Map.Map("id");
            marker.DispatchEvent(new Components.Map.Map("id"), new HtmlMarkerJsEventArgs());
            Assert.NotNull(marker.Options);
        }

        [Fact]
        public void Should_DispatchClickEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "click";
            marker.OnClick += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchContextMenuEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "contextmenu";
            marker.OnContextMenu += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "drag";
            marker.OnDrag += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragEndEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "dragend";
            marker.OnDragEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragStartEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "dragstart";
            marker.OnDragStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchKeyDownEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "keydown";
            marker.OnKeyDown += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchKeyPressEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "keypress";
            marker.OnKeyPress += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchKeyUpEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "keyup";
            marker.OnKeyUp += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseDownEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mousedown";
            marker.OnMouseDown += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseEnterEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mouseenter";
            marker.OnMouseEnter += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
        }

        [Fact]
        public void Should_DispatchMouseLeaveEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mouseleave";
            marker.OnMouseLeave += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
        }

        [Fact]
        public void Should_DispatchMouseMoveEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mousemove";
            marker.OnMouseMove += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
        }

        [Fact]
        public void Should_DispatchMouseOutEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mouseout";
            marker.OnMouseOut += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseOverEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mouseover";
            marker.OnMouseOver += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseUpEvent()
        {
            var assertEvent = false;
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var map = new Components.Map.Map("id");
            var type = "mouseup";
            marker.OnMouseUp += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == type && eventArgs.HtmlMarker == marker;
            marker.DispatchEvent(map, new HtmlMarkerJsEventArgs { Type = type });
            Assert.True(assertEvent);
        }

        [Fact]
        public async void Should_TogglePopupAsync()
        {
            var assertEvent = false;
            var popupInvokeHelper = new PopupInvokeHelper(null);
            var marker = new HtmlMarker(new HtmlMarkerOptions {
                Popup = new HtmlMarkerPopup(new PopupOptions())
            }) {
                JSRuntime = _jsRuntime.Object,
                PopupInvokeHelper = popupInvokeHelper
            };

            marker.OnPopupToggled += () => assertEvent = true;
            await marker.TogglePopupAsync();
            Assert.True(assertEvent);
            Assert.True(marker.Options.Popup.HasBeenToggled);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.HtmlMarker.TogglePopup.ToHtmlMarkerNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == marker.Id
                && parameters[1] as string == marker.Options.Popup.Id
                && parameters[2] is IEnumerable<string>
                && parameters[2] is DotNetObjectReference<PopupInvokeHelper>
            )));
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_Not_TogglePopupAsync()
        {
            var assertEvent = false;
            var popupInvokeHelper = new PopupInvokeHelper(null);
            var marker = new HtmlMarker(new HtmlMarkerOptions()) {
                JSRuntime = _jsRuntime.Object,
                PopupInvokeHelper = popupInvokeHelper
            };

            marker.OnPopupToggled += () => assertEvent = true;
            await marker.TogglePopupAsync();
            Assert.False(assertEvent);

            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
