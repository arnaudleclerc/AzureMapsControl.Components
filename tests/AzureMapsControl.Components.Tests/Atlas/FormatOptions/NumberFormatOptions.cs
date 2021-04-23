namespace AzureMapsControl.Components.Tests.Atlas.FormatOptions
{
    using AzureMapsControl.Components.Atlas.FormatOptions;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class NumberFormatOptionsJsonConverterTests : JsonConverterTests<NumberFormatOptions>
    {
        public NumberFormatOptionsJsonConverterTests() : base(new NumberFormatOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var value = new NumberFormatOptions {
                Currency = "currency",
                CurrencyDisplay = "currencyDisplay",
                CurrencySign = "currencySign",
                LocaleMatcher = "localeMatcher",
                MaximumFractionDigits = 1,
                MaximumSignificantDigits = 2,
                MinimumFractionDigits = 3,
                MinimumIntegerDigits = 4,
                MinimumSignificantDigits = 5,
                Style = "style",
                UseGrouping = true
            };

            var expectedJson = "{"
                + "\"currency\":\"currency\""
                + ",\"currencyDisplay\":\"currencyDisplay\""
                + ",\"currencySign\":\"currencySign\""
                + ",\"localeMatcher\":\"localeMatcher\""
                + ",\"maximumFractionDigits\":1"
                + ",\"maximumSignificantDigits\":2"
                + ",\"minimumFractionDigits\":3"
                + ",\"minimumIntegerDigits\":4"
                + ",\"minimumSignificantDigits\":5"
                + ",\"style\":\"style\""
                + ",\"useGrouping\":true"
                + "}";

            TestAndAssertWrite(value, expectedJson);
        }

        [Fact]
        public void Should_NotWrite() => TestAndAssertEmptytWrite(null);
    }
}
