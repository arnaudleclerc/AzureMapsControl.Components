namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(DataSourceOptionsJsonConverter))]
    public sealed class DataSourceOptions : SourceOptions
    {
        /// <summary>
        /// The size of the buffer around each tile.
        /// A buffer value of 0 will provide better performance but will be more likely to generate artifacts when rendering.
        /// Larger buffers will produce left artifacts but will result in slower performance.
        /// </summary>
        public int? Buffer { get; set; }

        /// <summary>
        /// A boolean indicating if Point features in the source should be clustered or not.
        /// If set to true, points will be clustered together into groups by radius.
        /// </summary>
        public bool? Cluster { get; set; }

        /// <summary>
        /// The maximum zoom level in which to cluster points.
        /// </summary>
        public int? ClusterMaxZoom { get; set; }

        /// <summary>
        /// The radius of each cluster in pixels.
        /// </summary>
        public int? ClusterRadius { get; set; }

        /// <summary>
        /// Specifies whether to calculate line distance metrics.
        /// This is required for line layers that specify `lineGradient` values.
        /// </summary>
        public bool? LineMetrics { get; set; }

        /// <summary>
        /// Maximum zoom level at which to create vector tiles (higher means greater detail at high zoom levels).
        /// </summary>
        public double? MaxZoom { get; set; }

        /// <summary>
        /// The Douglas-Peucker simplification tolerance that is applied to the data when rendering (higher means simpler geometries and faster performance).
        /// </summary>
        public double? Tolerance { get; set; }
    }

    internal sealed class DataSourceOptionsJsonConverter : JsonConverter<DataSourceOptions>
    {
        public override DataSourceOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new DataSourceOptions();
            while (reader.TokenType != JsonTokenType.EndObject || depth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "buffer":
                            result.Buffer = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "cluster":
                            result.Cluster = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                            break;
                        case "clusterMaxZoom":
                            result.ClusterMaxZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "clusterRadius":
                            result.ClusterRadius = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "lineMetrics":
                            result.LineMetrics = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                            break;
                        case "maxZoom":
                            result.MaxZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                        case "tolerance":
                            result.Tolerance = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, DataSourceOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            if (value.Buffer.HasValue)
            {
                writer.WriteNumber("buffer", value.Buffer.Value);
            }

            if (value.Cluster.HasValue)
            {
                writer.WriteBoolean("cluster", value.Cluster.Value);
            }

            if (value.ClusterMaxZoom.HasValue)
            {
                writer.WriteNumber("clusterMaxZoom", value.ClusterMaxZoom.Value);
            }

            if (value.ClusterRadius.HasValue)
            {
                writer.WriteNumber("clusterRadius", value.ClusterRadius.Value);
            }

            if (value.LineMetrics.HasValue)
            {
                writer.WriteBoolean("lineMetrics", value.LineMetrics.Value);
            }

            if (value.MaxZoom.HasValue)
            {
                writer.WriteNumber("maxZoom", value.MaxZoom.Value);
            }

            if (value.Tolerance.HasValue)
            {
                writer.WriteNumber("tolerance", value.Tolerance.Value);
            }
            writer.WriteEndObject();
        }
    }
}
