namespace AzureMapsControl.Components.Atlas.FormatOptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas.FormatOptions.DateTime;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(DateTimeFormatOptionsJsonConverter))]
    public sealed class DateTimeFormatOptions
    {
        public LocaleMatcher LocaleMatcher { get; set; }
        public Weekday Weekday { get; set; }
        public Era Era { get; set; }
        public Year Year { get; set; }
        public Month Month { get; set; }
        public Day Day { get; set; }
        public Hour Hour { get; set; }
        public Minute Minute { get; set; }
        public Second Second { get; set; }
        public TimeZoneName TimeZoneName { get; set; }
        public FormatMatcher FormatMatcher { get; set; }
        public bool? Hour12 { get; set; }
        public string TimeZone { get; set; }
    }

    internal class DateTimeFormatOptionsJsonConverter : JsonConverter<DateTimeFormatOptions>
    {
        public override DateTimeFormatOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, DateTimeFormatOptions value, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                writer.WriteStartObject();
                var localeMatcher = value.LocaleMatcher.ToString();
                if (!string.IsNullOrWhiteSpace(localeMatcher))
                {
                    writer.WriteString("localeMatcher", localeMatcher);
                }

                var weekday = value.Weekday.ToString();
                if (!string.IsNullOrWhiteSpace(weekday))
                {
                    writer.WriteString("weekday", weekday);
                }

                var era = value.Era.ToString();
                if (!string.IsNullOrWhiteSpace(era))
                {
                    writer.WriteString("era", era);
                }

                var year = value.Year.ToString();
                if (!string.IsNullOrWhiteSpace(year))
                {
                    writer.WriteString("year", year);
                }

                var month = value.Month.ToString();
                if (!string.IsNullOrWhiteSpace(month))
                {
                    writer.WriteString("month", month);
                }

                var day = value.Day.ToString();
                if (!string.IsNullOrWhiteSpace(day))
                {
                    writer.WriteString("day", day);
                }

                var hour = value.Hour.ToString();
                if (!string.IsNullOrWhiteSpace(hour))
                {
                    writer.WriteString("hour", hour);
                }

                var minute = value.Minute.ToString();
                if (!string.IsNullOrWhiteSpace(minute))
                {
                    writer.WriteString("minute", minute);
                }

                var second = value.Second.ToString();
                if (!string.IsNullOrWhiteSpace(second))
                {
                    writer.WriteString("second", second);
                }

                var timeZoneName = value.TimeZoneName.ToString();
                if (!string.IsNullOrWhiteSpace(timeZoneName))
                {
                    writer.WriteString("timeZoneName", timeZoneName);
                }

                var formatMatcher = value.FormatMatcher.ToString();
                if (!string.IsNullOrWhiteSpace(formatMatcher))
                {
                    writer.WriteString("formatMatcher", formatMatcher);
                }

                if (value.Hour12.HasValue)
                {
                    writer.WriteBoolean("hour12", value.Hour12.Value);
                }

                if (value.TimeZone is not null)
                {
                    writer.WriteString("timeZone", value.TimeZone);
                }

                writer.WriteEndObject();
            }
        }
    }
}
