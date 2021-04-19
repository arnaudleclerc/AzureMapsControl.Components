namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Markers;

    using Microsoft.JSInterop;

    internal class HtmlMarkerLayerCallbackInvokeHelper
    {
        private readonly Func<string, Position, IDictionary<string, object>, HtmlMarker> _markerCallback;
        private readonly Func<string, Position, IDictionary<string, object>, Task<HtmlMarker>> _markerCallbackAsync;

        public HtmlMarkerLayerCallbackInvokeHelper(Func<string, Position, IDictionary<string, object>, HtmlMarker> markerCallback) => _markerCallback = markerCallback;

        public HtmlMarkerLayerCallbackInvokeHelper(Func<string, Position, IDictionary<string, object>, Task<HtmlMarker>> markerCallback) => _markerCallbackAsync = markerCallback;

        [JSInvokable]
        public async Task<HtmlMarkerCreationOptions> InvokeMarkerCallbackAsync(string id, Position position, IDictionary<string, object> properties)
        {
            HtmlMarker marker = null;
            if (_markerCallbackAsync != null)
            {
                marker = await _markerCallbackAsync.Invoke(id, position, properties);
            }
            else
            {
                marker = _markerCallback.Invoke(id, position, properties);
            }

            Console.WriteLine("Returning marker");
            return new HtmlMarkerCreationOptions {
                Id = marker.Id,
                Options = marker.Options,
                Events = marker.EventActivationFlags.EnabledEvents,
                PopupOptions = marker.Options?.Popup != null ? new HtmlMarkerPopupCreationOptions {
                    Events = marker.Options.Popup.EventActivationFlags.EnabledEvents,
                    Id = marker.Options.Popup.Id,
                    Options = marker.Options.Popup.Options
                } : null
            };
        }
    }
}
