namespace AzureMapsControl.Components.Traffic
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains the configuration to display traffic on the map
    /// </summary>

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(TrafficOptionsJsonConverter))]
    public sealed class TrafficOptions
    {
        /// <summary>
        /// The type of traffic flow to display
        /// </summary>
        public TrafficFlow Flow { get; set; }

        /// <summary>
        /// Whether to display incidents on the map.
        /// </summary>
        public bool? Incidents { get; set; }
    }

    internal sealed class TrafficOptionsJsonConverter : JsonConverter<TrafficOptions>
    {
        public override TrafficOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var trafficOptions = new TrafficOptions();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    trafficOptions.Flow = TrafficFlow.FromString(reader.GetString());
                }
                else if (reader.TokenType == JsonTokenType.True)
                {
                    trafficOptions.Incidents = true;
                }
            }
            return trafficOptions;
        }
        public override void Write(Utf8JsonWriter writer, TrafficOptions value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("flow", value.Flow.ToString());
            if(value.Incidents.HasValue)
            {
                writer.WriteBoolean("incidents", value.Incidents.Value);
            }
            writer.WriteEndObject();
        }
    }
}
