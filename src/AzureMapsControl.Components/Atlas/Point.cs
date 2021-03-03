namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class Point : Geometry<Position>
    {
        public Point() : base() { }

        public Point(Position coordinates) : base(coordinates) { }
        public Point(string id) : base(id) { }

        public Point(string id, Position coordinates) : base(id) => Coordinates = coordinates;

        internal override string GetGeometryType() => "Point";
    }
}
