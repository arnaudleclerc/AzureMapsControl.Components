namespace AzureMapsControl.Components.Controls
{
    public sealed class ScalebarControlUnits
    {
        private readonly string _units;

        public static ScalebarControlUnits Imperial = new ScalebarControlUnits("imperial");
        public static ScalebarControlUnits Metric = new ScalebarControlUnits("metric");
        public static ScalebarControlUnits Meters = new ScalebarControlUnits("meters");
        public static ScalebarControlUnits Kilometers = new ScalebarControlUnits("kilometers");
        public static ScalebarControlUnits Yards = new ScalebarControlUnits("yards");
        public static ScalebarControlUnits Feet = new ScalebarControlUnits("feet");
        public static ScalebarControlUnits Miles = new ScalebarControlUnits("miles");
        public static ScalebarControlUnits NauticalMiles = new ScalebarControlUnits("nauticalMiles");

        private ScalebarControlUnits(string units) => _units = units;

        public override string ToString() => _units;
    }
}
