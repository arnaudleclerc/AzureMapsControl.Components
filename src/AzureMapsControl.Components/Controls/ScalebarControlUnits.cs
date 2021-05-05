namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct ScaleBarControlUnits
    {
        private readonly string _units;

        public static readonly ScaleBarControlUnits Imperial = new ScaleBarControlUnits("imperial");
        public static readonly ScaleBarControlUnits Metric = new ScaleBarControlUnits("metric");
        public static readonly ScaleBarControlUnits Meters = new ScaleBarControlUnits("meters");
        public static readonly ScaleBarControlUnits Kilometers = new ScaleBarControlUnits("kilometers");
        public static readonly ScaleBarControlUnits Yards = new ScaleBarControlUnits("yards");
        public static readonly ScaleBarControlUnits Feet = new ScaleBarControlUnits("feet");
        public static readonly ScaleBarControlUnits Miles = new ScaleBarControlUnits("miles");
        public static readonly ScaleBarControlUnits NauticalMiles = new ScaleBarControlUnits("nauticalMiles");

        private ScaleBarControlUnits(string units) => _units = units;

        public override string ToString() => _units;
    }
}
