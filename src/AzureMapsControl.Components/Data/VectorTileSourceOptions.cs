namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options for a `VectorTileSource`.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(VectorTileSourceOptionsJsonConverter))]
    public sealed class VectorTileSourceOptions : SourceOptions
    {
        /// <summary>
        /// A bounding box that specifies where tiles are available.
        /// When specified, no tiles outside of the bounding box will be requested.
        /// </summary>
        public BoundingBox Bounds { get; set; }

        /// <summary>
        /// Specifies is the tile systems y coordinate uses the OSGeo Tile Map Services which reverses the Y coordinate axis.
        /// </summary>
        public bool? IsTMS { get; set; }

        /// <summary>
        ///  An integer specifying the maximum zoom level to render the layer at.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level to render the layer at.
        /// </summary>
        public int? MinZoom { get; set; }

        /// <summary>
        /// An array of one or more tile source URLs.
        /// </summary>
        public IEnumerable<string> Tiles { get; set; }

        /// <summary>
        /// An integer value that specifies the width and height dimensions of the map tiles.
        /// For a seamless experience, the tile size must by a multiplier of 2. (i.e. 256, 512, 1024…).
        /// </summary>
        public int? TileSize { get; set; }

        /// <summary>
        /// A URL to a TileJSON resource.
        /// </summary>
        public string Url { get; set; }
    }

    internal sealed class VectorTileSourceOptionsJsonConverter : JsonConverter<VectorTileSourceOptions>
    {
        public override VectorTileSourceOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var originalDepth = reader.CurrentDepth;
            var result = new VectorTileSourceOptions();
            while (reader.TokenType != JsonTokenType.EndObject || originalDepth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "bounds":
                            result.Bounds = reader.TokenType == JsonTokenType.Null ? null : JsonSerializer.Deserialize<BoundingBox>(ref reader, options);
                            break;
                        case "isTMS":
                            result.IsTMS = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                            break;
                        case "maxZoom":
                            result.MaxZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "minZoom":
                            result.MinZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "tiles":
                            if (reader.TokenType != JsonTokenType.Null)
                            {
                                var tiles = new List<string>();
                                while (reader.TokenType != JsonTokenType.EndArray)
                                {
                                    reader.Read();
                                    if (reader.TokenType == JsonTokenType.String)
                                    {
                                        tiles.Add(reader.GetString());
                                    }
                                }
                                result.Tiles = tiles;
                            }
                            break;
                        case "tileSize":
                            result.TileSize = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "url":
                            result.Url = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                            break;
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, VectorTileSourceOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            if (value.Bounds is not null)
            {
                writer.WritePropertyName("bounds");
                JsonSerializer.Serialize(writer, value.Bounds, options);
            }

            if (value.IsTMS.HasValue)
            {
                writer.WriteBoolean("isTMS", value.IsTMS.Value);
            }

            if (value.MaxZoom.HasValue)
            {
                writer.WriteNumber("maxZoom", value.MaxZoom.Value);
            }

            if (value.MinZoom.HasValue)
            {
                writer.WriteNumber("minZoom", value.MinZoom.Value);
            }

            if (value.Tiles is not null)
            {
                writer.WritePropertyName("tiles");
                writer.WriteStartArray();
                foreach (var tile in value.Tiles)
                {
                    writer.WriteStringValue(tile);
                }
                writer.WriteEndArray();
            }

            if (value.TileSize.HasValue)
            {
                writer.WriteNumber("tileSize", value.TileSize.Value);
            }

            if (value.Url is not null)
            {
                writer.WriteString("url", value.Url);
            }

            writer.WriteEndObject();
        }
    }
}
