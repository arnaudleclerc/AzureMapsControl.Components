namespace AzureMapsControl.Components.Tests.Popups
{
    using System.Collections.Generic;
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas.FormatOptions;
    using AzureMapsControl.Components.Atlas.FormatOptions.DateTime;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class PopupTemplateJsonConverterTests : JsonConverterTests<PopupTemplate>
    {
        public PopupTemplateJsonConverterTests() : base(new PopupTemplateJsonConverter()) { }

        [Fact]
        public void Should_WriteStringTemplate()
        {
            var value = new PopupTemplate {
                Content = new Components.Atlas.Either<string, System.Collections.Generic.IEnumerable<Components.Atlas.PropertyInfo>, System.Collections.Generic.IEnumerable<Components.Atlas.Either<string, System.Collections.Generic.IEnumerable<Components.Atlas.PropertyInfo>>>>
                ("template"),
                DateFormat = new DateTimeFormatOptions {
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
                },
                DetectHyperlinks = true,
                FillColor = "fillColor",
                HyperlinkFormat = new HyperLinkFormatOptions {
                    IsImage = true,
                    Label = "label",
                    Scheme = "scheme",
                    Target = HyperLinkFormatOptionsTarget.Blank
                },
                NumberFormat = new NumberFormatOptions {
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
                },
                SandboxContent = true,
                SingleDescription = true,
                TextColor = "textColor",
                Title = "title"
            };

            var expectedJson = JsonSerializer.Serialize(value);
            TestAndAssertWrite(value, expectedJson);
        }

        [Fact]
        public void Should_WritePropertyInfoTemplate()
        {
            var value = new PopupTemplate {
                Content = new Components.Atlas.Either<string, System.Collections.Generic.IEnumerable<Components.Atlas.PropertyInfo>, System.Collections.Generic.IEnumerable<Components.Atlas.Either<string, System.Collections.Generic.IEnumerable<Components.Atlas.PropertyInfo>>>>
                (new[] {
                    new Components.Atlas.PropertyInfo {
                        PropertyPath = "createDate",
                        Label = "Created Date"
                    },
                    new Components.Atlas.PropertyInfo {
                        PropertyPath = "dateNumber",
                        Label = "Formatted date from number",
                        DateFormat = new Components.Atlas.FormatOptions.DateTimeFormatOptions {
                            Weekday = Components.Atlas.FormatOptions.DateTime.Weekday.Long,
                            Year = Components.Atlas.FormatOptions.DateTime.Year.Numeric,
                            Month = Components.Atlas.FormatOptions.DateTime.Month.Long,
                            Day = Components.Atlas.FormatOptions.DateTime.Day.Numeric,
                            TimeZone = "UTC",
                            TimeZoneName = Components.Atlas.FormatOptions.DateTime.TimeZoneName.Short
                        }
                    },
                    new Components.Atlas.PropertyInfo {
                        PropertyPath = "url",
                        Label = "Code samples",
                        HideLabel = true,
                        HyperlinkFormat = new Components.Atlas.FormatOptions.HyperLinkFormatOptions {
                            Label = "Go to code samples!",
                            Target = Components.Atlas.FormatOptions.HyperLinkFormatOptionsTarget.Blank
                        }
                    },
                    new Components.Atlas.PropertyInfo {
                        PropertyPath = "email",
                        Label = "Email Us",
                        HideLabel = true,
                        HyperlinkFormat = new Components.Atlas.FormatOptions.HyperLinkFormatOptions {
                            Target = Components.Atlas.FormatOptions.HyperLinkFormatOptionsTarget.Blank,
                            Scheme = "mailto:"
                        }
                    }
                }),
                DateFormat = new DateTimeFormatOptions {
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
                },
                DetectHyperlinks = true,
                FillColor = "fillColor",
                HyperlinkFormat = new HyperLinkFormatOptions {
                    IsImage = true,
                    Label = "label",
                    Scheme = "scheme",
                    Target = HyperLinkFormatOptionsTarget.Blank
                },
                NumberFormat = new NumberFormatOptions {
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
                },
                SandboxContent = true,
                SingleDescription = true,
                TextColor = "textColor",
                Title = "title"
            };

            var expectedJson = JsonSerializer.Serialize(value);
            TestAndAssertWrite(value, expectedJson);
        }

        [Fact]
        public void Should_WriteMultiContentTemplate()
        {
            var value = new PopupTemplate {
                Content = new Components.Atlas.Either<string, System.Collections.Generic.IEnumerable<Components.Atlas.PropertyInfo>, System.Collections.Generic.IEnumerable<Components.Atlas.Either<string, System.Collections.Generic.IEnumerable<Components.Atlas.PropertyInfo>>>>
                (new[] {
                new Components.Atlas.Either<string, IEnumerable<Components.Atlas.PropertyInfo>>
                ("This template has two pieces of content; a string template with placeholders and a array of property info which renders a full width image.<br/><br/> - Value 1 = {value1}<br/> - Value 2 = {value2/subValue}<br/> - Array value [2] = {arrayValue/2}"),
                new Components.Atlas.Either<string, IEnumerable<Components.Atlas.PropertyInfo>>
                (
                    new [] {
                        new Components.Atlas.PropertyInfo {
                            PropertyPath = "imageLink",
                            Label = "Image",
                            HideLabel = true,
                            HyperlinkFormat = new Components.Atlas.FormatOptions.HyperLinkFormatOptions {
                                IsImage = true
                            }
                        }
                    }
                )}),
                DateFormat = new DateTimeFormatOptions {
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
                },
                DetectHyperlinks = true,
                FillColor = "fillColor",
                HyperlinkFormat = new HyperLinkFormatOptions {
                    IsImage = true,
                    Label = "label",
                    Scheme = "scheme",
                    Target = HyperLinkFormatOptionsTarget.Blank
                },
                NumberFormat = new NumberFormatOptions {
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
                },
                SandboxContent = true,
                SingleDescription = true,
                TextColor = "textColor",
                Title = "title"
            };

            var expectedJson = JsonSerializer.Serialize(value);
            TestAndAssertWrite(value, expectedJson);
        }

        [Fact]
        public void Should_NotWrite() => TestAndAssertEmptytWrite(null);
    }
}
