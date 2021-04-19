namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Markers;

    using Microsoft.JSInterop;

    internal class HtmlMarkerLayerCallbackInvokeHelper
    {
        private readonly Func<string, Position, IDictionary<string, JsonElement>, HtmlMarker> _markerCallback;
        private readonly Func<string, Position, IDictionary<string, JsonElement>, Task<HtmlMarker>> _markerCallbackAsync;

        public HtmlMarkerLayerCallbackInvokeHelper(Func<string, Position, IDictionary<string, JsonElement>, HtmlMarker> markerCallback) => _markerCallback = markerCallback;

        public HtmlMarkerLayerCallbackInvokeHelper(Func<string, Position, IDictionary<string, JsonElement>, Task<HtmlMarker>> markerCallback) => _markerCallbackAsync = markerCallback;

        [JSInvokable]
        public async Task<HtmlMarkerCreationOptions> InvokeMarkerCallbackAsync(string id, Position position, IDictionary<string, JsonElement> properties)
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
