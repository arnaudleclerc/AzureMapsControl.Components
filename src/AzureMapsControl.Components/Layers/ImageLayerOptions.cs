namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(ImageLayerOptionsJsonConverter))]
    public sealed class ImageLayerOptions : MediaLayerOptions
    {
        /// <summary>
        /// URL to an image to overlay. Images hosted on other domains must have CORs enabled.
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// An array of positions for the corners of the image listed in clockwise order: [top left, top right, bottom right, bottom left].
        /// </summary>
        public IEnumerable<Position> Coordinates { get; internal set; }

        public ImageLayerOptions() { }

        public ImageLayerOptions(string url, IEnumerable<Position> coordinates)
        {
            Url = url;
            Coordinates = coordinates;
        }
    }

    internal sealed class ImageLayerOptionsJsonConverter : MediaLayerOptionsJsonConverter<ImageLayerOptions>
    {
        public override ImageLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new ImageLayerOptions();
            while (reader.TokenType != JsonTokenType.EndObject || depth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (IsMediaLayerOptionsProperty(propertyName))
                    {
                        ReadMediaLayerOptionsProperty(propertyName, reader, result);
                    }
                    else
                    {
                        reader.Read();
                        switch (propertyName)
                        {
                            case "url":
                                result.Url = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                                break;

                            case "coordinates":
                                if (reader.TokenType == JsonTokenType.StartArray)
                                {
                                    var coordinates = new List<Position>();
                                    reader.Read();
                                    while (reader.TokenType == JsonTokenType.StartArray)
                                    {
                                        coordinates.Add(JsonSerializer.Deserialize<Position>(ref reader, options));
                                        reader.Read();
                                    }
                                    result.Coordinates = coordinates;
                                }
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, ImageLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            WriteMediaLayerOptionsProperties(writer, value, options);

            if (value.Url is not null)
            {
                writer.WriteString("url", value.Url);
            }

            if (value.Coordinates is not null)
            {
                writer.WritePropertyName("coordinates");
                writer.WriteStartArray();
                foreach (var position in value.Coordinates)
                {
                    JsonSerializer.Serialize(writer, position, options);
                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
