namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    [JsonConverter(typeof(OverviewMapControlJsonConverter))]
    public sealed class OverviewMapControl : Control<OverviewMapControlOptions>
    {
        internal override string Type => "overviewmap";
        internal override int Order => int.MaxValue;
        internal IMapJsRuntime JsRuntime { get; set; }
        internal ILogger Logger { get; set; }

        public OverviewMapControl(OverviewMapControlOptions options = null, ControlPosition position = default) : base(options, position) { }

        /// <summary>
        /// Update the options of the control
        /// </summary>
        /// <param name="update">Update to apply to the options</param>
        /// <returns></returns>
        public async Task UpdateAsync(Action<OverviewMapControlOptions> update)
        {
            if (Options is null)
            {
                Options = new OverviewMapControlOptions();
            }

            update(Options);

            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AzureMap_AddControlsAsync, "OverviewMapControl - UpdateAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.OverviewMapControl_UpdateAsync, $"Id: {Id}");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.OverviewMapControl_UpdateAsync, $"Type: {Type}");
            await JsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateControl.ToCoreNamespace(), this);
        }
    }

    internal class OverviewMapControlJsonConverter : JsonConverter<OverviewMapControl>
    {
        public override OverviewMapControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, OverviewMapControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, OverviewMapControl value)
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
                if (value.Options.Height.HasValue)
                {
                    writer.WriteNumber("height", value.Options.Height.Value);
                }
                if (value.Options.Interactive.HasValue)
                {
                    writer.WriteBoolean("interactive", value.Options.Interactive.Value);
                }
                if (value.Options.MapStyle is not null)
                {
                    writer.WriteString("mapStyle", value.Options.MapStyle.ToString());
                }
                if (value.Options.MarkerOptions is not null)
                {
                    writer.WriteStartObject("markerOptions");
                    if (value.Options.MarkerOptions.Anchor is not null)
                    {
                        writer.WriteString("anchor", value.Options.MarkerOptions.Anchor.ToString());
                    }
                    if (value.Options.MarkerOptions.Color is not null)
                    {
                        writer.WriteString("color", value.Options.MarkerOptions.Color);
                    }
                    if (value.Options.MarkerOptions.Draggable.HasValue)
                    {
                        writer.WriteBoolean("draggable", value.Options.MarkerOptions.Draggable.Value);
                    }
                    if (value.Options.MarkerOptions.HtmlContent is not null)
                    {
                        writer.WriteString("htmlContent", value.Options.MarkerOptions.HtmlContent);
                    }
                    if (value.Options.MarkerOptions.PixelOffset is not null)
                    {
                        writer.WritePropertyName("pixelOffset");
                        writer.WriteStartArray();
                        writer.WriteNumberValue(value.Options.MarkerOptions.PixelOffset.X);
                        writer.WriteNumberValue(value.Options.MarkerOptions.PixelOffset.Y);
                        writer.WriteEndArray();
                    }
                    if (value.Options.MarkerOptions.Position is not null)
                    {
                        writer.WritePropertyName("position");
                        writer.WriteStartArray();
                        writer.WriteNumberValue(value.Options.MarkerOptions.Position.Longitude);
                        writer.WriteNumberValue(value.Options.MarkerOptions.Position.Latitude);
                        if (value.Options.MarkerOptions.Position.Elevation.HasValue)
                        {
                            writer.WriteNumberValue(value.Options.MarkerOptions.Position.Elevation.Value);
                        }
                        writer.WriteEndArray();
                    }
                    if (value.Options.MarkerOptions.SecondaryColor is not null)
                    {
                        writer.WriteString("secondaryColor", value.Options.MarkerOptions.SecondaryColor);
                    }
                    if (value.Options.MarkerOptions.Text is not null)
                    {
                        writer.WriteString("text", value.Options.MarkerOptions.Text);
                    }
                    if (value.Options.MarkerOptions.Visible.HasValue)
                    {
                        writer.WriteBoolean("visible", value.Options.MarkerOptions.Visible.Value);
                    }
                    writer.WriteEndObject();
                }
                if (value.Options.Minimized.HasValue)
                {
                    writer.WriteBoolean("minimized", value.Options.Minimized.Value);
                }
                if (value.Options.Overlay is not null)
                {
                    writer.WriteString("overlay", value.Options.Overlay.ToString());
                }
                if (value.Options.ShowToggle.HasValue)
                {
                    writer.WriteBoolean("showToggle", value.Options.ShowToggle.Value);
                }
                if (value.Options.Style is not null)
                {
                    writer.WriteString("style", value.Options.Style.HasFirstChoice ? value.Options.Style.FirstChoice.ToString() : value.Options.Style.SecondChoice);
                }
                if (value.Options.SyncBearingPitch.HasValue)
                {
                    writer.WriteBoolean("syncBearingPitch", value.Options.SyncBearingPitch.Value);
                }
                if (value.Options.SyncZoom.HasValue)
                {
                    writer.WriteBoolean("syncZoom", value.Options.SyncZoom.Value);
                }
                if (value.Options.Visible.HasValue)
                {
                    writer.WriteBoolean("visible", value.Options.Visible.Value);
                }
                if (value.Options.Width.HasValue)
                {
                    writer.WriteNumber("width", value.Options.Width.Value);
                }
                if (value.Options.Zoom.HasValue)
                {
                    writer.WriteNumber("zoom", value.Options.Zoom.Value);
                }
                if (value.Options.ZoomOffset.HasValue)
                {
                    writer.WriteNumber("zoomOffset", value.Options.ZoomOffset.Value);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
