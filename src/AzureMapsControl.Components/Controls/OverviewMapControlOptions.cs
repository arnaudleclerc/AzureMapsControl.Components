namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(OverviewMapControlOptionsJsonConverter))]
    public sealed class OverviewMapControlOptions : IControlOptions
    {
        /// <summary>
        /// The height of the overview map in pixels.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Specifies if the overview map is interactive.
        /// </summary>
        public bool? Interactive { get; set; }

        /// <summary>
        /// The name of the style to use when rendering the map.
        /// </summary>
        public MapStyle MapStyle { get; set; }

        /// <summary>
        /// Options for customizing the marker overlay. If the draggable option of the marker it enabled, the map will center over the marker location after it has been dragged to a new location.
        /// </summary>
        public HtmlMarkerOptions MarkerOptions { get; set; }

        /// <summary>
        /// Specifies if the overview map is minimized or not
        /// </summary>
        public bool? Minimized { get; set; }

        /// <summary>
        /// Specifies the type of information to overlay on top of the map.
        /// </summary>
        public OverviewMapControlOverlay Overlay { get; set; }

        /// <summary>
        /// Specifies if a toggle button for minimizing the overview map should be displayed or not.
        /// </summary>
        public bool? ShowToggle { get; set; }

        /// <summary>
        /// The style of the control. Can be an instance of ControlStyle, or any CSS3 color.
        /// </summary>
        public Either<ControlStyle, string> Style { get; set; }

        /// <summary>
        /// Specifies if bearing and pitch changes should be synchronized.
        /// </summary>
        public bool? SyncBearingPitch { get; set; }

        /// <summary>
        /// Specifies if zoom level changes should be synchronized.
        /// </summary>
        public bool? SyncZoom { get; set; }

        /// <summary>
        /// Specifies if the overview map control is visible or not
        /// </summary>
        public bool? Visible { get; set; }

        /// <summary>
        /// The width of the overview map in pixels
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Zoom level to set on overview map when not synchronizing zoom level changes.
        /// </summary>
        public int? Zoom { get; set; }

        /// <summary>
        /// The number of zoom levels to offset from the parent map zoom level when synchronizing zoom level changes.
        /// </summary>
        public int? ZoomOffset { get; set; }
    }

    internal class OverviewMapControlOptionsJsonConverter : JsonConverter<OverviewMapControlOptions>
    {
        public override OverviewMapControlOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, OverviewMapControlOptions value, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                writer.WriteStartObject();
                if (value.Height.HasValue)
                {
                    writer.WriteNumber("height", value.Height.Value);
                }
                if (value.Interactive.HasValue)
                {
                    writer.WriteBoolean("interactive", value.Interactive.Value);
                }
                if (value.MapStyle is not null)
                {
                    writer.WriteString("mapStyle", value.MapStyle.ToString());
                }
                if (value.MarkerOptions is not null)
                {
                    writer.WriteStartObject("markerOptions");
                    if (value.MarkerOptions.Anchor is not null)
                    {
                        writer.WriteString("anchor", value.MarkerOptions.Anchor.ToString());
                    }
                    if (value.MarkerOptions.Color is not null)
                    {
                        writer.WriteString("color", value.MarkerOptions.Color);
                    }
                    if (value.MarkerOptions.Draggable.HasValue)
                    {
                        writer.WriteBoolean("draggable", value.MarkerOptions.Draggable.Value);
                    }
                    if (value.MarkerOptions.HtmlContent is not null)
                    {
                        writer.WriteString("htmlContent", value.MarkerOptions.HtmlContent);
                    }
                    if (value.MarkerOptions.PixelOffset is not null)
                    {
                        writer.WritePropertyName("pixelOffset");
                        writer.WriteStartArray();
                        writer.WriteNumberValue(value.MarkerOptions.PixelOffset.X);
                        writer.WriteNumberValue(value.MarkerOptions.PixelOffset.Y);
                        writer.WriteEndArray();
                    }
                    if (value.MarkerOptions.Position is not null)
                    {
                        writer.WritePropertyName("position");
                        writer.WriteStartArray();
                        writer.WriteNumberValue(value.MarkerOptions.Position.Longitude);
                        writer.WriteNumberValue(value.MarkerOptions.Position.Latitude);
                        if (value.MarkerOptions.Position.Elevation.HasValue)
                        {
                            writer.WriteNumberValue(value.MarkerOptions.Position.Elevation.Value);
                        }
                        writer.WriteEndArray();
                    }
                    if (value.MarkerOptions.SecondaryColor is not null)
                    {
                        writer.WriteString("secondaryColor", value.MarkerOptions.SecondaryColor);
                    }
                    if (value.MarkerOptions.Text is not null)
                    {
                        writer.WriteString("text", value.MarkerOptions.Text);
                    }
                    if (value.MarkerOptions.Visible.HasValue)
                    {
                        writer.WriteBoolean("visible", value.MarkerOptions.Visible.Value);
                    }
                    writer.WriteEndObject();
                }
                if (value.Minimized.HasValue)
                {
                    writer.WriteBoolean("minimized", value.Minimized.Value);
                }
                if (value.Overlay is not null)
                {
                    writer.WriteString("overlay", value.Overlay.ToString());
                }
                if (value.ShowToggle.HasValue)
                {
                    writer.WriteBoolean("showToggle", value.ShowToggle.Value);
                }
                if (value.Style is not null)
                {
                    writer.WriteString("style", value.Style.HasFirstChoice ? value.Style.FirstChoice.ToString() : value.Style.SecondChoice);
                }
                if (value.SyncBearingPitch.HasValue)
                {
                    writer.WriteBoolean("syncBearingPitch", value.SyncBearingPitch.Value);
                }
                if (value.SyncZoom.HasValue)
                {
                    writer.WriteBoolean("syncZoom", value.SyncZoom.Value);
                }
                if (value.Visible.HasValue)
                {
                    writer.WriteBoolean("visible", value.Visible.Value);
                }
                if (value.Width.HasValue)
                {
                    writer.WriteNumber("width", value.Width.Value);
                }
                if (value.Zoom.HasValue)
                {
                    writer.WriteNumber("zoom", value.Zoom.Value);
                }
                if (value.ZoomOffset.HasValue)
                {
                    writer.WriteNumber("zoomOffset", value.ZoomOffset.Value);
                }
                writer.WriteEndObject();
            }
        }
    }
}
