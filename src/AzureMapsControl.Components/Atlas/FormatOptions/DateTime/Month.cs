namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct Month
    {
        private readonly string _type;

        public static readonly Month Numeric = new Month("numeric");
        public static readonly Month TwoDigits = new Month("2-digits");
        public static readonly Month Long = new Month("long");
        public static readonly Month Short = new Month("short");
        public static readonly Month Narrow = new Month("narrow");

        private Month(string type) => _type = type;

        public override string ToString() => _type;
    }
}
