namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.FullScreen;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    public delegate void FullScreenChanged(bool isFullScreen);

    /// <summary>
    /// A control that toggles the map or a specific container from its defined size to a fullscreen size.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FullScreenControl : Control<FullScreenControlOptions>
    {
        private readonly FullScreenEventInvokeHelper _eventInvokeHelper;
        private readonly FullScreenEventActivationFlags _eventFlags;

        internal event ControlDisposed OnDisposed;
        public event FullScreenChanged OnFullScreenChanged;

        internal override string Type => "fullscreen";

        internal override int Order => 0;

        internal IMapJsRuntime JsRuntime { get; set; }
        internal ILogger Logger { get; set; }

        /// <summary>
        /// Flag indicating if the control has been disposed
        /// </summary>
        public bool Disposed { get; private set; }

        public FullScreenControl(FullScreenControlOptions options = null, ControlPosition position = default, FullScreenEventActivationFlags eventFlags = null) : base(options, position)
        {
            _eventInvokeHelper = new(DispatchEventAsync);
            _eventFlags = eventFlags;
        }

        /// <summary>
        /// Disposes the control.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask DisposeAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.FullScreenControl_DisposeAsync, "FullScreenControl - DisposeAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.FullScreenControl_DisposeAsync, $"Id : {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.Dispose.ToFullScreenControlNamespace(), Id);
            Disposed = true;
            OnDisposed?.Invoke();
        }

        /// <summary>
        /// Sets the options of the control. 
        /// </summary>
        /// <param name="update">Update to apply on the options</param>
        /// <returns></returns>
        /// <exception cref="ControlDisposedException">The control has already been disposed</exception>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public async ValueTask SetOptionsAsync(Action<FullScreenControlOptions> update)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.FullScreenControl_DisposeAsync, "FullScreenControl - SetOptionsAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.FullScreenControl_DisposeAsync, $"Id : {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            if (Options is null)
            {
                Options = new();
            }

            update(Options);

            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.SetOptions.ToFullScreenControlNamespace(), Id, Options);
        }

        /// <summary>
        /// Checks if the map or specified container is in fullscreen mode or not.
        /// </summary>
        /// <returns>True if the map or specified container is in fullscreen mode, otherwise false</returns>
        /// <exception cref="ControlDisposedException">The control has already been disposed</exception>
        /// <exception cref="ComponentNotAddedToMapException">The control has not been added to the map</exception>
        public async ValueTask<bool> IsFullScreenAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.FullScreenControl_DisposeAsync, "FullScreenControl - IsFullScreenAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.FullScreenControl_DisposeAsync, $"Id : {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JsRuntime.InvokeAsync<bool>(Constants.JsConstants.Methods.FullScreenControl.IsFullScreen.ToFullScreenControlNamespace(), Id);
        }

        internal async ValueTask AddEventsAsync()
        {
            if (_eventFlags?.EnabledEvents is not null && _eventFlags.EnabledEvents.Any())
            {
                Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.FullScreenControl_AddEventsAsync, "FullScreenControl - AddEventsAsync");
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.FullScreenControl_AddEventsAsync, $"Id: {Id}");
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.FullScreenControl_AddEventsAsync, $"Events: {_eventFlags.EnabledEvents}");

                await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.AddEvents.ToFullScreenControlNamespace(),
                    Id,
                    _eventFlags.EnabledEvents,
                    DotNetObjectReference.Create(_eventInvokeHelper));
            }
        }

        private async ValueTask DispatchEventAsync(bool isFullScreen) => await Task.Run(() => OnFullScreenChanged?.Invoke(isFullScreen));

        private void EnsureNotDisposed()
        {
            if (Disposed)
            {
                throw new ComponentDisposedException();
            }
        }

        private void EnsureJsRuntimeExists()
        {
            if (JsRuntime is null)
            {
                throw new ComponentNotAddedToMapException();
            }
        }
    }

    internal class FullScreenControlJsonConverter : JsonConverter<FullScreenControl>
    {
        public override FullScreenControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, FullScreenControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, FullScreenControl value)
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
                writer.WritePropertyName("options");
                writer.WriteStartObject();
                if (value.Options.Container is not null)
                {
                    writer.WriteString("container", value.Options.Container);
                }
                if (value.Options.HideIfUnsupported.HasValue)
                {
                    writer.WriteBoolean("hideIfUnsupported", value.Options.HideIfUnsupported.Value);
                }
                if (value.Options.Style is not null)
                {
                    writer.WriteString("style", value.Options.Style.HasFirstChoice ? value.Options.Style.FirstChoice.ToString() : value.Options.Style.SecondChoice);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
