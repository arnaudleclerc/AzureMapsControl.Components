namespace AzureMapsControl.Components.Markers
{
    using System;

    using AzureMapsControl.Components.Map;

    public delegate void HtmlMarkerEvent(HtmlMarkerEventArgs eventArgs);

    /// <summary>
    /// HTML Marker which can be added directly to the map
    /// </summary>
    public sealed class HtmlMarker
    {
        internal string Id { get; }

        /// <summary>
        /// Options of the marker
        /// </summary>
        public HtmlMarkerOptions Options { get; }

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options of the HtmlMarker</param>
        public HtmlMarker(HtmlMarkerOptions options) : this(options, HtmlMarkerEventActivationFlags.None()) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options of the HtmlMarker</param>
        /// <param name="eventActivationFlags">Events to activate on the marker</param>
        public HtmlMarker(HtmlMarkerOptions options, HtmlMarkerEventActivationFlags eventActivationFlags)
        {
            Id = Guid.NewGuid().ToString();
            Options = options;
            EventActivationFlags = eventActivationFlags;
        }

        internal void DispatchEvent(Map map, string type)
        {
            switch(type)
            {
                case "click":
                    OnClick?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "contextmenu":
                    OnContextMenu?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "dblclick":
                    OnDblClick?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "drag":
                    OnDrag?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "dragend":
                    OnDragEnd?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "dragstart":
                    OnDragStart?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "keydown":
                    OnKeyDown?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "keypress":
                    OnKeyPress?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "keyup":
                    OnKeyUp?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mousedown":
                    OnMouseDown?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mouseenter":
                    OnMouseEnter?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mouseleave":
                    OnMouseLeave?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mousemove":
                    OnMouseMove?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mouseout":
                    OnMouseOut?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mouseover":
                    OnMouseOver?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;

                case "mouseup":
                    OnMouseUp?.Invoke(new HtmlMarkerEventArgs(map, type, this));
                    break;
            }
        }

    }
}
