namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<RoutePoint>))]
    public sealed class RoutePoint : Point
    {
        public DateTime Timestamp { get; set; }
        public RoutePoint() { }

        public RoutePoint(Position coordinates, DateTime timestamp) : base(coordinates) => Timestamp = timestamp;
    }
}
