namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class RoutePoint : Point
    {
        public DateTime Timestamp { get; set; }
        public RoutePoint() { }

        public RoutePoint(Position coordinates, DateTime timestamp) : base(coordinates) => Timestamp = timestamp;
    }
}
