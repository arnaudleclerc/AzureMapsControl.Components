namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class Point : Geometry
    {
        public Position Coordinates { get; set; }

        public Point() { }

        public Point(Position coordinates) => Coordinates = coordinates;

        internal override string GetGeometryType() => "Point";
    }
}
