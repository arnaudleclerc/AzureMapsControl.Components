namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct Weekday
    {
        private readonly string _type;

        public static readonly Weekday Long = new Weekday("long");
        public static readonly Weekday Short = new Weekday("short");
        public static readonly Weekday Narrow = new Weekday("narrow");

        private Weekday(string type) => _type = type;

        public override string ToString() => _type;
    }
}
