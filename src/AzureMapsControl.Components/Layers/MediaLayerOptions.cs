namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Options used when rendering canvas, image, raster tile, and video layers
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(MediaLayerOptionsJsonConverter))]
    public abstract class MediaLayerOptions : LayerOptions
    {
        /// <summary>
        /// A number between -1 and 1 that increases or decreases the contrast of the overlay.
        /// </summary>
        public double? Contrast { get; set; }

        /// <summary>
        /// The duration in milliseconds of a fade transition when a new tile is added.
        /// Must be greater or equal to 0.
        /// </summary>
        public int? FadeDuration { get; set; }

        /// <summary>
        /// Rotates hues around the color wheel.
        /// A number in degrees.
        /// </summary>
        public int? HueRotation { get; set; }

        /// <summary>
        /// A number between 0 and 1 that increases or decreases the maximum brightness of the overlay.
        /// </summary>
        public double? MaxBrightness { get; set; }

        /// <summary>
        /// A number between 0 and 1 that increases or decreases the minimum brightness of the overlay.
        /// </summary>
        public double? MinBrightness { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the overlay will be drawn.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// A number between -1 and 1 that increases or decreases the saturation of the overlay.
        /// </summary>
        public double? Saturation { get; set; }
    }

    internal abstract class MediaLayerOptionsJsonConverter<TOptions> : LayerOptionsJsonConverter<TOptions>
        where TOptions : MediaLayerOptions
    {
        private static readonly string[] s_mediaLayerOptionsProperties = new[] { "contrast", "fadeDuration", "hueRotation", "maxBrightness", "minBrightness", "opacity", "saturation" };

        protected static bool IsMediaLayerOptionsProperty(string propertyName) => s_mediaLayerOptionsProperties.Contains(propertyName) || IsLayerOptionsProperty(propertyName);

        protected static void ReadMediaLayerOptionsProperty(string propertyName, Utf8JsonReader reader, TOptions value)
        {
            if (IsLayerOptionsProperty(propertyName))
            {
                ReadLayerOptionsProperty(propertyName, reader, value);
            }
            else
            {
                reader.Read();
                switch (propertyName)
                {
                    case "contrast":
                        value.Contrast = reader.TokenType == JsonTokenType.Null ? null : reader.GetDouble();
                        break;

                    case "fadeDuration":
                        value.FadeDuration = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                        break;

                    case "hueRotation":
                        value.HueRotation = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                        break;

                    case "maxBrightness":
                        value.MaxBrightness = reader.TokenType == JsonTokenType.Null ? null : reader.GetDouble();
                        break;

                    case "minBrightness":
                        value.MinBrightness = reader.TokenType == JsonTokenType.Null ? null : reader.GetDouble();
                        break;

                    case "opacity":
                        value.Opacity = reader.TokenType == JsonTokenType.Null ? null : reader.GetDouble();
                        break;

                    case "saturation":
                        value.Saturation = reader.TokenType == JsonTokenType.Null ? null : reader.GetDouble();
                        break;
                }
            }
        }

        protected static void WriteMediaLayerOptionsProperties(Utf8JsonWriter writer, TOptions value, JsonSerializerOptions options)
        {
            WriteLayerOptionsProperties(writer, value, options);

            if (value.Contrast.HasValue)
            {
                writer.WriteNumber("contrast", value.Contrast.Value);
            }

            if (value.FadeDuration.HasValue)
            {
                writer.WriteNumber("fadeDuration", value.FadeDuration.Value);
            }

            if (value.HueRotation.HasValue)
            {
                writer.WriteNumber("hueRotation", value.HueRotation.Value);
            }

            if (value.MaxBrightness.HasValue)
            {
                writer.WriteNumber("maxBrightness", value.MaxBrightness.Value);
            }

            if (value.MinBrightness.HasValue)
            {
                writer.WriteNumber("minBrightness", value.MinBrightness.Value);
            }

            if (value.Opacity.HasValue)
            {
                writer.WriteNumber("opacity", value.Opacity.Value);
            }

            if (value.Saturation.HasValue)
            {
                writer.WriteNumber("saturation", value.Saturation.Value);
            }
        }
    }

    internal sealed class MediaLayerOptionsJsonConverter : MediaLayerOptionsJsonConverter<MediaLayerOptions>
    {
        public override MediaLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, MediaLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is ImageLayerOptions imageLayerOptions)
            {
                JsonSerializer.Serialize(writer, imageLayerOptions, options);
            }
            else if (value is TileLayerOptions tileLayerOptions)
            {
                JsonSerializer.Serialize(writer, tileLayerOptions, options);
            }
        }
    }
}
