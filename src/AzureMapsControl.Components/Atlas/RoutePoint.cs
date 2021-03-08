namespace AzureMapsControl.Components.Atlas
{
    using System;

    public sealed class RoutePoint : Point
    {
        public DateTime Timestamp { get; set; }
        public RoutePoint() { }

        public RoutePoint(Position coordinates, DateTime timestamp) : base(coordinates) => Timestamp = timestamp;

        public RoutePoint(string id, Position coordinates, DateTime timestamp) : base(id, coordinates) => Timestamp = timestamp;
    }
}
