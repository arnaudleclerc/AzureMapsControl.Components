namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Geolocation;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    public delegate void GeolocationSuccess(GeolocationSuccessEventArgs eventArgs);
    public delegate void GeolocationError(GeolocationErrorEventArgs eventArgs);

    public sealed class GeolocationControl : Control<GeolocationControlOptions>
    {
        private readonly GeolocationEventInvokeHelper _eventInvokeHelper;
        private readonly GeolocationEventActivationFlags _eventFlags;

        internal override string Type => "geolocation";

        internal override int Order => 0;

        internal IMapJsRuntime JsRuntime { get; set; }
        internal ILogger Logger { get; set; }

        /// <summary>
        /// Flag indicating if the control has been disposed
        /// </summary>
        public bool Disposed { get; private set; }

        internal event ControlDisposed OnDisposed;
        public event GeolocationSuccess GeolocationSuccess;
        public event GeolocationError GeolocationError;

        public GeolocationControl(GeolocationControlOptions options = null, ControlPosition position = default, GeolocationEventActivationFlags eventFlags = null) : base(options, position)
        {
            _eventInvokeHelper = new GeolocationEventInvokeHelper(DispatchEventAsync);
            _eventFlags = eventFlags;
        }

        /// <summary>
        /// Get the last known position from the geolocation control.
        /// </summary>
        /// <returns>Feature containing the last known position</returns>
        /// <exception cref="ControlDisposedException">The control has already been disposed</exception>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public async ValueTask<Feature<Point>> GetLastKnownPositionAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GeolocationControl_GetLastKnownPositionAsync, "GeolocationControl - GetLastKnownPositionAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GeolocationControl_GetLastKnownPositionAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JsRuntime.InvokeAsync<Feature<Point>>(Constants.JsConstants.Methods.GeolocationControl.GetLastKnownPosition.ToGeolocationControlNamespace(), Id);
        }

        /// <summary>
        /// Disposes the control.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ControlDisposedException">The control has already been disposed</exception>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public async ValueTask DisposeAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GeolocationControl_DisposeAsync, "GeolocationControl - DisposeAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GeolocationControl_DisposeAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), Id);
            Disposed = true;
            OnDisposed?.Invoke();
        }

        /// <summary>
        /// Sets the options of the geolocation control.
        /// </summary>
        /// <param name="update">Update to apply on the options</param>
        /// <returns></returns>
        /// <exception cref="ControlDisposedException">The control has already been disposed</exception>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public async ValueTask SetOptionsAsync(Action<GeolocationControlOptions> update)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GeolocationControl_SetOptionsAsync, "GeolocationControl - SetOptionsAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GeolocationControl_SetOptionsAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            if (Options is null)
            {
                Options = new GeolocationControlOptions();
            }

            update(Options);

            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.SetOptions.ToGeolocationControlNamespace(), Id, Options);
        }

        internal async ValueTask AddEventsAsync()
        {
            if (_eventFlags?.EnabledEvents is not null && _eventFlags.EnabledEvents.Any())
            {
                Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GeolocationControl_AddEventsAsync, "GeolocationControl - AddEventsAsync");
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GeolocationControl_AddEventsAsync, $"Id: {Id}");
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GeolocationControl_AddEventsAsync, $"Events: {_eventFlags.EnabledEvents}");

                await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.AddEvents.ToGeolocationControlNamespace(),
                    Id,
                    _eventFlags.EnabledEvents,
                    DotNetObjectReference.Create(_eventInvokeHelper));
            }
        }

        private async ValueTask DispatchEventAsync(GeolocationJsEventArgs eventArgs)
        {
            await Task.Run(() => {
                switch (eventArgs.Type)
                {
                    case "geolocationerror":
                        GeolocationError?.Invoke(new GeolocationErrorEventArgs(eventArgs));
                        break;
                    case "geolocationsuccess":
                        GeolocationSuccess?.Invoke(new GeolocationSuccessEventArgs(eventArgs));
                        break;
                }
            });
        }

        private void EnsureNotDisposed()
        {
            if (Disposed)
            {
                throw new ControlDisposedException();
            }
        }

        private void EnsureJsRuntimeExists()
        {
            if (JsRuntime is null)
            {
                throw new Exceptions.ComponentNotAddedToMapException();
            }
        }
    }

    internal class GeolocationControlJsonConverter : JsonConverter<GeolocationControl>
    {
        public override GeolocationControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, GeolocationControl value, JsonSerializerOptions options) => Write(writer, value);

        public static void Write(Utf8JsonWriter writer, GeolocationControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WriteString("type", value.Type);
            if (value.Position.ToString() != default(ControlPosition).ToString())
            {
                writer.WriteString("position", value.Position.ToString());
            }
            if (value.Options is not null)
            {
                writer.WriteStartObject("options");
                if (value.Options.CalculateMissingValues.HasValue)
                {
                    writer.WriteBoolean("calculateMissingValues", value.Options.CalculateMissingValues.Value);
                }

                if (!string.IsNullOrWhiteSpace(value.Options.MarkerColor))
                {
                    writer.WriteString("markerColor", value.Options.MarkerColor);
                }

                if (value.Options.MaxZoom.HasValue)
                {
                    writer.WriteNumber("maxZoom", value.Options.MaxZoom.Value);
                }

                if (value.Options.PositionOptions is not null)
                {
                    writer.WriteStartObject("positionOptions");
                    if (value.Options.PositionOptions.EnableHighAccuracy.HasValue)
                    {
                        writer.WriteBoolean("enableHighAccuracy", value.Options.PositionOptions.EnableHighAccuracy.Value);
                    }

                    if (value.Options.PositionOptions.MaximumAge.HasValue)
                    {
                        writer.WriteNumber("maximumAge", value.Options.PositionOptions.MaximumAge.Value);
                    }

                    if (value.Options.PositionOptions.Timeout.HasValue)
                    {
                        writer.WriteNumber("timeout", value.Options.PositionOptions.Timeout.Value);
                    }

                    writer.WriteEndObject();
                }

                if (value.Options.ShowUserLocation.HasValue)
                {
                    writer.WriteBoolean("showUserLocation", value.Options.ShowUserLocation.Value);
                }

                if (value.Options.Style.ToString() != default(ControlPosition).ToString())
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }

                if (value.Options.TrackUserLocation.HasValue)
                {
                    writer.WriteBoolean("trackUserLocation", value.Options.TrackUserLocation.Value);
                }

                if (value.Options.UpdateMapCamera.HasValue)
                {
                    writer.WriteBoolean("updateMapCamera", value.Options.UpdateMapCamera.Value);
                }

                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
