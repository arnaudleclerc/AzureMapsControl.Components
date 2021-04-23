namespace AzureMapsControl.Components.Atlas.FormatOptions.DateTime
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct FormatMatcher
    {
        private readonly string _type;

        public static readonly FormatMatcher Basic = new FormatMatcher("basic");
        public static readonly FormatMatcher BestFit = new FormatMatcher("best fit");

        private FormatMatcher(string type) => _type = type;

        public override string ToString() => _type;
    }
}
