namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Point : Geometry<Position>
    {
        public Point() : base() { }

        public Point(Position coordinates) : base(coordinates) { }

        internal override string GetGeometryType() => "Point";
    }
}
