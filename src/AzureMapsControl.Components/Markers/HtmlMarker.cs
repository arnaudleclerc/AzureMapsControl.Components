namespace AzureMapsControl.Components.Markers
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal delegate void HtmlMarkerPopupToggledEvent();
    public delegate void HtmlMarkerEvent(HtmlMarkerEventArgs eventArgs);

    /// <summary>
    /// HTML Marker which can be added directly to the map
    /// </summary>
    public sealed class HtmlMarker
    {
        internal string Id { get; }

        internal IMapJsRuntime JSRuntime { get; set; }
        internal PopupInvokeHelper PopupInvokeHelper { get; set; }
        internal ILogger Logger { get; set; }

        /// <summary>
        /// Options of the marker
        /// </summary>
        public HtmlMarkerOptions Options { get; private set; }

        /// <summary>
        /// Events to activate on the marker
        /// </summary>
        public HtmlMarkerEventActivationFlags EventActivationFlags { get; set; }

        /// <summary>
        /// Fired when a pointing device is pressed and released at the same point on the marker.
        /// </summary>
        public event HtmlMarkerEvent OnClick;

        /// <summary>
        /// Fired when the right button of the mouse is clicked on the marker.
        /// </summary>
        public event HtmlMarkerEvent OnContextMenu;

        /// <summary>
        /// Fired when a pointing device is clicked twice at the same point on the marker.
        /// </summary>
        public event HtmlMarkerEvent OnDblClick;

        /// <summary>
        /// Fired repeatedly during a "drag to pan" interaction on the HTML marker.
        /// </summary>
        public event HtmlMarkerEvent OnDrag;

        /// <summary>
        /// Fired when a "drag to pan" interaction ends on the HTML marker.
        /// </summary>
        public event HtmlMarkerEvent OnDragEnd;

        /// <summary>
        /// Fired when a "drag to pan" interaction starts on the HTML marker.
        /// </summary>
        public event HtmlMarkerEvent OnDragStart;

        /// <summary>
        /// Fired when key is pressed down on the HTML marker.
        /// </summary>
        public event HtmlMarkerEvent OnKeyDown;

        /// <summary>
        /// Fired when key is pressed on the HTML marker.
        /// </summary>
        public event HtmlMarkerEvent OnKeyPress;

        /// <summary>
        /// Fired when key is pressed up on the HTML marker.
        /// </summary>
        public event HtmlMarkerEvent OnKeyUp;

        /// <summary>
        /// Fired when a pointing device is pressed within the HTML marker or when on top of an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseDown;

        /// <summary>
        /// Fired when a pointing device is initially moved over the HTML marker or an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseEnter;

        /// <summary>
        /// Fired when a pointing device is moved out the HTML marker or an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseLeave;

        /// <summary>
        /// Fired when a pointing device is moved within the HTML marker or an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseMove;

        /// <summary>
        /// Fired when a point device leaves the HTML marker's canvas our leaves an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseOut;

        /// <summary>
        /// Fired when a pointing device is moved over the HTML marker or an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseOver;

        /// <summary>
        /// Fired when a pointing device is released within the HTML Marker or when on top of an element.
        /// </summary>
        public event HtmlMarkerEvent OnMouseUp;

        internal event HtmlMarkerPopupToggledEvent OnPopupToggled;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options of the HtmlMarker</param>
        public HtmlMarker(HtmlMarkerOptions options) : this(options, HtmlMarkerEventActivationFlags.None()) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id of the HTML Marker</param>
        /// <param name="options">Options of the HTML Marker</param>
        public HtmlMarker(string id, HtmlMarkerOptions options) : this(id, options, HtmlMarkerEventActivationFlags.None()) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options of the HtmlMarker</param>
        /// <param name="eventActivationFlags">Events to activate on the marker</param>
        public HtmlMarker(HtmlMarkerOptions options, HtmlMarkerEventActivationFlags eventActivationFlags) : this(Guid.NewGuid().ToString(), options, eventActivationFlags) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id of the HTML Marker</param>
        /// <param name="options">Options of the HTML Marker</param>
        /// <param name="eventActivationFlags">Events to activate on the marker</param>
        public HtmlMarker(string id, HtmlMarkerOptions options, HtmlMarkerEventActivationFlags eventActivationFlags)
        {
            Id = id;
            Options = options;
            EventActivationFlags = eventActivationFlags;
        }

        /// <summary>
        /// Toggles the popup attached to the marker.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public async ValueTask TogglePopupAsync()
        {
            if (Options?.Popup != null)
            {
                Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.HtmlMarker_TogglePopupAsync, "Calling TogglePopupAsync");

                if (JSRuntime is null)
                {
                    throw new Exceptions.ComponentNotAddedToMapException();
                }

                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.HtmlMarker.TogglePopup.ToHtmlMarkerNamespace(), Id, Options.Popup.Id, Options.Popup.EventActivationFlags.EnabledEvents, DotNetObjectReference.Create(PopupInvokeHelper));
                Options.Popup.HasBeenToggled = true;
                Options.Popup.IsRemoved = false;
                OnPopupToggled?.Invoke();
            }
        }

        internal void DispatchEvent(Map map, HtmlMarkerJsEventArgs eventArgs)
        {
            if (eventArgs.Options != null)
            {
                var popupOptions = Options.Popup;
                Options = eventArgs.Options;
                Options.Popup = popupOptions;
            }

            switch (eventArgs.Type)
            {
                case "click":
                    OnClick?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "contextmenu":
                    OnContextMenu?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "dblclick":
                    OnDblClick?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "drag":
                    OnDrag?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "dragend":
                    OnDragEnd?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "dragstart":
                    OnDragStart?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "keydown":
                    OnKeyDown?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "keypress":
                    OnKeyPress?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "keyup":
                    OnKeyUp?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mousedown":
                    OnMouseDown?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mouseenter":
                    OnMouseEnter?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mouseleave":
                    OnMouseLeave?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mousemove":
                    OnMouseMove?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mouseout":
                    OnMouseOut?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mouseover":
                    OnMouseOver?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;

                case "mouseup":
                    OnMouseUp?.Invoke(new HtmlMarkerEventArgs(map, eventArgs.Type, this));
                    break;
            }
        }

    }
}
