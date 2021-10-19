namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering raster tiled images in a TileLayer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(TileLayerOptionsJsonConverter))]
    public sealed class TileLayerOptions : MediaLayerOptions
    {
        public TileLayerOptions() { }
        public TileLayerOptions(Uri tileUrl) => TileUrl = tileUrl.ToString();
        public TileLayerOptions(string tileUrl) => TileUrl = tileUrl;

        /// <summary>
        /// A bounding box that specifies where tiles are available.
        /// When specified, no tiles outside of the bounding box will be requested.
        /// </summary>
        public BoundingBox Bounds { get; set; }

        /// <summary>
        /// Specifies if the tile systems coordinates uses the Tile Map Services specification, which reverses the Y coordinate axis.
        /// </summary>
        public bool? IsTMS { get; set; }

        /// <summary>
        /// An integer specifying the maximum zoom level in which tiles are available from the tile source.
        /// </summary>
        public int? MaxSourceZoom { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level in which tiles are available from the tile source.
        /// </summary>
        public int? MinSourceZoom { get; set; }

        /// <summary>
        /// An array of subdomain values to apply to the tile URL.
        /// </summary>
        public IEnumerable<string> Subdomains { get; set; }

        /// <summary>
        /// An integer value that specifies the width and height dimensions of the map tiles.
        /// </summary>
        public int? TileSize { get; set; }

        /// <summary>
        /// A http/https URL to a TileJSON resource or a tile URL template that uses the following parameters:
        /// {x}: X position of the tile. Usually also needs {y} and {z}.
        /// {y}: Y position of the tile. Usually also needs {x} and {z}.
        /// {z}: Zoom level of the tile. Usually also needs {x} and {y}.
        /// {quadkey}: Tile quadKey id based on the Bing Maps tile system naming convention.
        /// {bbox-epsg-3857}: A bounding box string with the format {west},{south},{east},{north} in the EPSG 4325 Spacial Reference System.
        /// {subdomain}: A placeholder where the subdomain values if specified will be added.
        /// </summary>
        public string TileUrl { get; set; }
    }

    internal sealed class TileLayerOptionsJsonConverter : MediaLayerOptionsJsonConverter<TileLayerOptions>
    {
        public override TileLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new TileLayerOptions();
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
                            case "bounds":
                                result.Bounds = reader.TokenType == JsonTokenType.Null ? null : JsonSerializer.Deserialize<BoundingBox>(ref reader, options);
                                break;

                            case "isTMS":
                                result.IsTMS = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                                break;

                            case "maxSourceZoom":
                                result.MaxSourceZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                                break;

                            case "minSourceZoom":
                                result.MinSourceZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                                break;

                            case "subdomains":
                                if (reader.TokenType == JsonTokenType.StartArray)
                                {
                                    var subdomains = new List<string>();
                                    reader.Read();
                                    while (reader.TokenType != JsonTokenType.EndArray)
                                    {
                                        subdomains.Add(reader.GetString());
                                        reader.Read();
                                    }
                                    result.Subdomains = subdomains;
                                }
                                break;

                            case "tileSize":
                                result.TileSize = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                                break;

                            case "tileUrl":
                                result.TileUrl = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, TileLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            WriteMediaLayerOptionsProperties(writer, value, options);

            if (value.Bounds is not null)
            {
                writer.WritePropertyName("bounds");
                JsonSerializer.Serialize(writer, value.Bounds, options);
            }

            if (value.IsTMS.HasValue)
            {
                writer.WriteBoolean("isTMS", value.IsTMS.Value);
            }

            if (value.MaxSourceZoom.HasValue)
            {
                writer.WriteNumber("maxSourceZoom", value.MaxSourceZoom.Value);
            }

            if (value.MinSourceZoom.HasValue)
            {
                writer.WriteNumber("minSourceZoom", value.MinSourceZoom.Value);
            }

            if (value.Subdomains is not null)
            {
                writer.WritePropertyName("subdomains");
                writer.WriteStartArray();
                foreach (var subdomain in value.Subdomains)
                {
                    writer.WriteStringValue(subdomain);
                }
                writer.WriteEndArray();
            }

            if (value.TileSize.HasValue)
            {
                writer.WriteNumber("tileSize", value.TileSize.Value);
            }

            if (value.TileUrl is not null)
            {
                writer.WriteString("tileUrl", value.TileUrl);
            }

            writer.WriteEndObject();
        }
    }
}
