namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used to customize the text in a SymbolLayer
    /// </summary>

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(TextOptionsJsonConverter))]
    public sealed class TextOptions
    {
        /// <summary>
        /// Specifies if the text will be visible if it collides with other symbols.
        /// </summary>
        public bool? AllowOverlap { get; set; }

        /// <summary>
        /// Specifies which part of the icon is placed closest to the icons anchor position on the map.
        /// </summary>
        public ExpressionOrString Anchor { get; set; }

        /// <summary>
        /// Specifies the name of a property on the features to use for a text label.
        /// </summary>
        public ExpressionOrString TextField { get; set; }

        /// <summary>
        /// The font stack to use for displaying text.
        /// </summary>
        public ExpressionOrStringArray Font { get; set; }

        /// <summary>
        /// Specifies if the other symbols are allowed to collide with the text.
        /// </summary>
        public bool? IgnorePlacement { get; set; }

        /// <summary>
        /// Specifies an offset distance of the icon from its anchor in ems.
        /// </summary>
        public Expression Offset { get; set; }

        /// <summary>
        /// Specifies if the text can be hidden if it is overlapped by another symbol.
        /// </summary>
        public bool? Optional { get; set; }

        /// <summary>
        /// Specifies the orientation of the text when the map is pitched.
        /// </summary>
        public PitchAlignment PitchAlignment { get; set; }

        /// <summary>
        /// The amount to rotate the text clockwise in degrees.
        /// </summary>
        public ExpressionOrNumber Rotation { get; set; }

        /// <summary>
        /// In combination with the `placement` property of the `SymbolLayerOptions`, specifies the rotation behavior of the individual glyphs forming the text.
        /// </summary>
        public PitchAlignment RotationAlignment { get; set; }

        /// <summary>
        /// The size of the font in pixels.
        /// </summary>
        public ExpressionOrNumber Size { get; set; }

        /// <summary>
        /// The color of the text.
        /// </summary>
        public ExpressionOrString Color { get; set; }

        /// <summary>
        /// The halo's fadeout distance towards the outside in pixels.
        /// </summary>
        public ExpressionOrNumber HaloBlur { get; set; }

        /// <summary>
        /// The color of the text's halo, which helps it stand out from backgrounds.
        /// </summary>
        public ExpressionOrString HaloColor { get; set; }

        /// <summary>
        /// The distance of the halo to the font outline in pixels.
        /// </summary>
        public ExpressionOrNumber HaloWidth { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the text will be drawn.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }
    }

    internal sealed class TextOptionsJsonConverter : JsonConverter<TextOptions>
    {
        public override TextOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new TextOptions();
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

        public override void Write(Utf8JsonWriter writer, TextOptions value, JsonSerializerOptions options)
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

            if (value.Color is not null)
            {
                writer.WritePropertyName("color");
                JsonSerializer.Serialize(writer, value.Color, options);
            }

            if (value.Font is not null)
            {
                writer.WritePropertyName("font");
                JsonSerializer.Serialize(writer, value.Font, options);
            }

            if (value.HaloBlur is not null)
            {
                writer.WritePropertyName("haloBlur");
                JsonSerializer.Serialize(writer, value.HaloBlur, options);
            }

            if (value.HaloColor is not null)
            {
                writer.WritePropertyName("haloColor");
                JsonSerializer.Serialize(writer, value.HaloColor, options);
            }

            if (value.HaloWidth is not null)
            {
                writer.WritePropertyName("haloWidth");
                JsonSerializer.Serialize(writer, value.HaloWidth, options);
            }

            if (value.IgnorePlacement.HasValue)
            {
                writer.WriteBoolean("ignorePlacement", value.IgnorePlacement.Value);
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

            if (value.TextField is not null)
            {
                writer.WritePropertyName("textField");
                JsonSerializer.Serialize(writer, value.TextField, options);
            }

            writer.WriteEndObject();
        }
    }
}
