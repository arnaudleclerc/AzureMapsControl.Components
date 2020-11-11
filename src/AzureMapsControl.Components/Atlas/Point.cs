namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class Point : Geometry
    {
        public Position Coordinates { get; set; }

        public Point() : base() { }

        public Point(Position coordinates) : base(Guid.NewGuid().ToString()) => Coordinates = coordinates;
        public Point(string id) : base(id) { }

        public Point(string id, Position coordinates) : base(id) => Coordinates = coordinates;

        internal override string GetGeometryType() => "Point";
    }
}
