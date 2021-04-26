namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct Year
    {
        private readonly string _type;

        public static readonly Year Numeric = new Year("numeric");
        public static readonly Year TwoDigits = new Year("2-digits");

        private Year(string type) => _type = type;

        public override string ToString() => _type;
    }
}
