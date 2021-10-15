namespace AzureMapsControl.Components.Tests.Atlas.FormatOptions
{
    using AzureMapsControl.Components.Atlas.FormatOptions;
    using AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class DateTimeFormatOptionsJsonConverterTests : JsonConverterTests<DateTimeFormatOptions>
    {
        public DateTimeFormatOptionsJsonConverterTests(): base(new DateTimeFormatOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var value = new DateTimeFormatOptions {
                Day = Day.Numeric,
                Era = Era.Long,
                FormatMatcher = FormatMatcher.Basic,
                Hour = Hour.Numeric,
                Hour12 = true,
                LocaleMatcher = LocaleMatcher.BestFit,
                Minute = Minute.Numeric,
                Month = Month.Numeric,
                Second = Second.Numeric,
                TimeZone = "timeZone",
                TimeZoneName = TimeZoneName.Long,
                Weekday = Weekday.Long,
                Year = Year.Numeric
            };

            var expectedJson = "{"
                + "\"localeMatcher\":\"" + LocaleMatcher.BestFit.ToString() + "\""
                + "\",weekday\":\"" + Weekday.Long.ToString() + "\""
                + "\",era\":\"" + Era.Long.ToString() + "\""
                + "\",year\":\"" + Year.Numeric.ToString() + "\""
                + "\",month\":\"" + Month.Numeric.ToString() + "\""
                + "\",day\":\"" + Day.Numeric.ToString() + "\""
                + "\",hour\":\"" + Hour.Numeric.ToString() + "\""
                + "\",minute\":\"" + Minute.Numeric.ToString() + "\""
                + "\",second\":\"" + Second.Numeric.ToString() + "\""
                + "\",timeZoneName\":\"" + TimeZoneName.Long.ToString() + "\""
                + "\",formatMatcher\":\"" + FormatMatcher.Basic.ToString() + "\""
                + "\",hour12\":true"
                + "\",timeZone\":\"timeZone\""
                + "}";

            TestAndAssertWrite(value, expectedJson);
        }

        [Fact]
        public void Should_NotWrite() => TestAndAssertEmptyWrite(null);
    }
}
