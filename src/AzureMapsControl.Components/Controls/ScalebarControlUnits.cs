namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class ScaleBarControlUnits
    {
        private readonly string _units;

        public static ScaleBarControlUnits Imperial = new ScaleBarControlUnits("imperial");
        public static ScaleBarControlUnits Metric = new ScaleBarControlUnits("metric");
        public static ScaleBarControlUnits Meters = new ScaleBarControlUnits("meters");
        public static ScaleBarControlUnits Kilometers = new ScaleBarControlUnits("kilometers");
        public static ScaleBarControlUnits Yards = new ScaleBarControlUnits("yards");
        public static ScaleBarControlUnits Feet = new ScaleBarControlUnits("feet");
        public static ScaleBarControlUnits Miles = new ScaleBarControlUnits("miles");
        public static ScaleBarControlUnits NauticalMiles = new ScaleBarControlUnits("nauticalMiles");

        private ScaleBarControlUnits(string units) => _units = units;

        public override string ToString() => _units;
    }
}
