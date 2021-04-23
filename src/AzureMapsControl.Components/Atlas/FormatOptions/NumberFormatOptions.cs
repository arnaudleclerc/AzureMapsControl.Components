namespace AzureMapsControl.Components.Atlas.FormatOptions
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class NumberFormatOptions
    {
        public string Currency { get; set; }
        public string CurrencyDisplay { get; set; }
        public string CurrencySign { get; set; }
        public string LocaleMatcher { get; set; }
        public int? MaximumFractionDigits { get; set; }
        public int? MaximumSignificantDigits { get; set; }
        public int? MinimumIntegerDigits { get; set; }
        public int? MinimumFractionDigits { get; set; }
        public int? MinimumSignificantDigits { get; set; }
        public string Style { get; set; }
        public bool? UseGrouping { get; set; }
    }
}
