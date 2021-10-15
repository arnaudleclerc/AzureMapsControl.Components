namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used to customize the icons in a SymbolLayer
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(IconOptionsJsonConverter))]
    public sealed class IconOptions
    {
        /// <summary>
        /// Specifies if the symbol icon can overlay other symbols on the map.
        /// </summary>
        public bool? AllowOverlap { get; set; }

        /// <summary>
        /// Specifies which part of the icon is placed closest to the icons anchor position on the map.
        /// </summary>
        public ExpressionOrString Anchor { get; set; }

        /// <summary>
        /// Specifies if other symbols can overlap this symbol.
        /// </summary>
        public bool? IgnorePlacement { get; set; }

        /// <summary>
        /// The name of the image in the map's image sprite to use for drawing the icon.
        /// </summary>
        public ExpressionOrString Image { get; set; }

        /// <summary>
        /// Specifies an offset distance of the icon from its anchor in pixels.
        /// </summary>
        public Expression Offset { get; set; }

        /// <summary>
        /// Specifies if a symbols icon can be hidden but its text displayed if it is overlapped with another symbol.
        /// </summary>
        public bool? Optional { get; set; }

        /// <summary>
        /// Specifies the orientation of the icon when the map is pitched.
        /// </summary>
        public PitchAlignment PitchAlignment { get; set; }

        /// <summary>
        /// The amount to rotate the icon clockwise in degrees
        /// </summary>
        public ExpressionOrNumber Rotation { get; set; }

        /// <summary>
        /// In combination with the placement property of a SymbolLayerOptions this determines the rotation behavior of icons.
        /// </summary>
        public PitchAlignment RotationAlignment { get; set; }

        /// <summary>
        /// Scales the original size of the icon by the provided factor.
        /// </summary>
        public ExpressionOrNumber Size { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the icon will be drawn.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }
    }

    internal sealed class IconOptionsJsonConverter : JsonConverter<IconOptions>
    {
        public override IconOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new IconOptions();
            while (reader.TokenType != JsonTokenType.EndObject || depth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "allowOverlap":
                            result.AllowOverlap = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                            break;
                        case "ignorePlacement":
                            result.IgnorePlacement = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                            break;
                        case "optional":
                            result.Optional = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                            break;
                        case "pitchAlignment":
                            result.PitchAlignment = reader.TokenType == JsonTokenType.Null ? default : PitchAlignment.FromString(reader.GetString());
                            break;
                        case "rotationAlignment":
                            result.RotationAlignment = reader.TokenType == JsonTokenType.Null ? default : PitchAlignment.FromString(reader.GetString());
                            break;
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, IconOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            if (value.AllowOverlap.HasValue)
            {
                writer.WriteBoolean("allowOverlap", value.AllowOverlap.Value);
            }

            if (value.Anchor is not null)
            {
                writer.WritePropertyName("anchor");
                JsonSerializer.Serialize(writer, value.Anchor, options);
            }

            if (value.IgnorePlacement.HasValue)
            {
                writer.WriteBoolean("ignorePlacement", value.IgnorePlacement.Value);
            }

            if (value.Image is not null)
            {
                writer.WritePropertyName("image");
                JsonSerializer.Serialize(writer, value.Image, options);
            }

            if (value.Offset is not null)
            {
                writer.WritePropertyName("offset");
                JsonSerializer.Serialize(writer, value.Offset, options);
            }

            if (value.Opacity is not null)
            {
                writer.WritePropertyName("opacity");
                JsonSerializer.Serialize(writer, value.Opacity, options);
            }

            if (value.Optional.HasValue)
            {
                writer.WriteBoolean("optional", value.Optional.Value);
            }

            if (value.PitchAlignment.ToString() != default(PitchAlignment).ToString())
            {
                writer.WriteString("pitchAlignment", value.PitchAlignment.ToString());
            }

            if (value.Rotation is not null)
            {
                writer.WritePropertyName("rotation");
                JsonSerializer.Serialize(writer, value.Rotation, options);
            }

            if (value.RotationAlignment.ToString() != default(PitchAlignment).ToString())
            {
                writer.WriteString("rotationAlignment", value.RotationAlignment.ToString());
            }

            if (value.Size is not null)
            {
                writer.WritePropertyName("size");
                JsonSerializer.Serialize(writer, value.Size, options);
            }

            writer.WriteEndObject();
        }
    }
}
