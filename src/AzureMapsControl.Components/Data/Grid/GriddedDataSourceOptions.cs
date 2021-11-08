namespace AzureMapsControl.Components.Data.Grid
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Atlas.Math;

    /// <summary>
    /// Options for a gridded data source.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GriddedDataSourceOptionsJsonConverter))]
    public sealed class GriddedDataSourceOptions : SourceOptions
    {
        /// <summary>
        /// Defines custom properties that are calculated using expressions against all the points within each grid cell and added to the properties of each grid cell polygon.
        /// </summary>
        public System.Collections.Generic.IDictionary<string, Expression> AggregateProperties { get; set; }

        /// <summary>
        /// The spatial width of each cell in the grid in the specified distance units.
        /// </summary>
        public double? CellWidth { get; set; }

        /// <summary>
        /// The latitude value used to calculate the pixel equivalent of the cellWidth.
        /// </summary>
        public double? CenterLatitude { get; set; }

        /// <summary>
        /// A number between 0 and 1 that specifies how much area a cell polygon should consume within the grid cell. 
        /// This applies a multiplier to the scale of all cells. If `scaleProperty` is specified, this will add additional scaling. 
        /// </summary>
        public double? Coverage { get; set; }

        /// <summary>
        /// The distance units of the cellWidth option.
        /// </summary>
        public DistanceUnits? DistanceUnits { get; set; }

        /// <summary>
        /// The shape of the data bin to generate.
        /// </summary>
        public GridType? GridType { get; set; }

        /// <summary>
        /// The minimium cell width to use by the coverage and scaling operations. Will be snapped to the `cellWidth` if greater than that value.
        /// </summary>
        public double? MinCellWidth { get; set; }

        /// <summary>
        /// Maximum zoom level at which to create vector tiles (higher means greater detail at high zoom levels).
        /// </summary>
        public double? MaxZoom { get; set; }

        /// <summary>
        /// A data driven expression that customizes how the scaling function is done. This expression has access to the properties of the cell (CellInfo) and the following two properties; 
        /// `min` - The minimium aggregate value across all cells in the data source.
        /// `max` - The maximium aggregate value across all cells in the data source.
        /// </summary>
        public Expression ScaleExpression { get; set; }

        /// <summary>
        /// The aggregate property to calculate the min/max values over the whole data set. Can be an aggregate property or `point_count`.
        /// </summary>
        public string ScaleProperty { get; set; }
    }

    internal sealed class GriddedDataSourceOptionsJsonConverter : JsonConverter<GriddedDataSourceOptions>
    {
        public override GriddedDataSourceOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var originalDepth = reader.CurrentDepth;
            var result = new GriddedDataSourceOptions();
            while (reader.TokenType != JsonTokenType.EndObject || originalDepth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "cellWidth":
                            result.CellWidth = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;

                        case "centerLatitude":
                            result.CenterLatitude = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;

                        case "coverage":
                            result.Coverage = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;

                        case "distanceUnits":
                            result.DistanceUnits = JsonSerializer.Deserialize<DistanceUnits>(ref reader, options);
                            break;

                        case "gridType":
                            result.GridType = JsonSerializer.Deserialize<GridType>(ref reader, options);
                            break;

                        case "minCellWidth":
                            result.MinCellWidth = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;

                        case "maxZoom":
                            result.MaxZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                            break;

                        case "scaleProperty":
                            result.ScaleProperty = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                            break;
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, GriddedDataSourceOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            if (value.AggregateProperties is not null)
            {
                writer.WritePropertyName("aggregateProperties");
                writer.WriteStartObject();
                foreach (var property in value.AggregateProperties)
                {
                    writer.WritePropertyName(property.Key);
                    JsonSerializer.Serialize(writer, property.Value, options);
                }
                writer.WriteEndObject();
            }

            if (value.CellWidth.HasValue)
            {
                writer.WriteNumber("cellWidth", value.CellWidth.Value);
            }

            if (value.CenterLatitude.HasValue)
            {
                writer.WriteNumber("centerLatitude", value.CenterLatitude.Value);
            }

            if (value.Coverage.HasValue)
            {
                writer.WriteNumber("coverage", value.Coverage.Value);
            }

            if (value.DistanceUnits.HasValue)
            {
                writer.WritePropertyName("distanceUnits");
                JsonSerializer.Serialize(writer, value.DistanceUnits.Value, options);
            }

            if (value.GridType.HasValue)
            {
                writer.WritePropertyName("gridType");
                JsonSerializer.Serialize(writer, value.GridType.Value, options);
            }

            if (value.MaxZoom.HasValue)
            {
                writer.WriteNumber("maxZoom", value.MaxZoom.Value);
            }

            if (value.MinCellWidth.HasValue)
            {
                writer.WriteNumber("minCellWidth", value.MinCellWidth.Value);
            }

            if (value.ScaleExpression is not null)
            {
                writer.WritePropertyName("scaleExpression");
                JsonSerializer.Serialize(writer, value.ScaleExpression, options);
            }

            if (value.ScaleProperty is not null)
            {
                writer.WriteString("scaleProperty", value.ScaleProperty);
            }
            writer.WriteEndObject();
        }
    }
}
