namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Atlas.FormatOptions;

    /// <summary>
    /// A layout template for a popup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(PopupTemplateJsonConverter))]
    public sealed class PopupTemplate
    {
        /// <summary>
        /// A HTML string for the main content of the popup that contains placeholders for properties of the feature it is being displayed for.
        /// Placeholders can be in the format "{propertyName}" or "{propertyName/subPropertyName}".
        /// </summary>
        public Either<string, IEnumerable<PropertyInfo>, IEnumerable<Either<string, IEnumerable<PropertyInfo>>>> Content { get; set; }

        /// <summary>
        /// If the property is a date object, these options specify how it should be formatted when displayed.
        /// </summary>
        public DateTimeFormatOptions DateFormat { get; set; }

        /// <summary>
        /// Specifies if hyperlinks and email addresses should automatically be detected and rendered as clickable links.
        /// </summary>
        public bool? DetectHyperlinks { get; set; }

        /// <summary>
        /// The background color of the popup template.
        /// </summary>
        public string FillColor { get; set; }

        /// <summary>
        /// Format options for hyperlink strings.
        /// </summary>
        public HyperLinkFormatOptions HyperlinkFormat { get; set; }

        /// <summary>
        /// If the property is a number, these options specify how it should be formatted when displayed.
        /// </summary>
        public NumberFormatOptions NumberFormat { get; set; }

        /// <summary>
        /// The default text color of the popup template.
        /// </summary>
        public string TextColor { get; set; }

        /// <summary>
        /// A HTML string for the title of the popup that contains placeholders for properties of the feature it is being displayed for.
        /// Placeholders can be in the format "{propertyName}" or "{propertyName/subPropertyName}".
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Specifies if content should be wrapped with a sandboxed iframe.
        /// Unless explicitly set to false, the content will be sandboxed within an iframe by default.
        /// When enabled, all content will be wrapped in a sandboxed iframe with scripts, forms, pointer lock and top navigation disabled.
        /// Popups will be allowed so that links can be opened in a new page or tab.
        /// Older browsers that don't support the srcdoc parameter on iframes will be limited to rendering a small amount of content.
        /// </summary>
        public bool? SandboxContent { get; set; }

        /// <summary>
        /// If a description is available, it will be written as the content rather than as a table of properties.
        /// </summary>
        public bool? SingleDescription { get; set; }
    }

    internal class PopupTemplateJsonConverter : JsonConverter<PopupTemplate>
    {
        public override PopupTemplate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, PopupTemplate value, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                writer.WriteStartObject();
                if (value.Content is not null)
                {
                    writer.WritePropertyName("content");
                    if (value.Content.HasFirstChoice)
                    {
                        writer.WriteStringValue(value.Content.FirstChoice);
                    }
                    else if (value.Content.HasSecondChoice)
                    {
                        JsonSerializer.Serialize(writer, value.Content.SecondChoice, options);
                    }
                    else
                    {
                        writer.WriteStartArray();
                        foreach (var choice in value.Content.ThirdChoice)
                        {
                            if (choice.HasFirstChoice)
                            {
                                writer.WriteStringValue(choice.FirstChoice);
                            }
                            else
                            {
                                JsonSerializer.Serialize(writer, choice.SecondChoice, options);
                            }
                        }
                        writer.WriteEndArray();
                    }
                }

                if (value.DateFormat is not null)
                {
                    writer.WritePropertyName("dateFormat");
                    JsonSerializer.Serialize(writer, value.DateFormat, options);
                }

                if (value.DetectHyperlinks.HasValue)
                {
                    writer.WriteBoolean("detectHyperlinks", value.DetectHyperlinks.Value);
                }

                if (value.FillColor is not null)
                {
                    writer.WriteString("fillColor", value.FillColor);
                }

                if (value.HyperlinkFormat is not null)
                {
                    writer.WritePropertyName("hyperlinkFormat");
                    JsonSerializer.Serialize(writer, value.HyperlinkFormat, options);
                }

                if (value.NumberFormat is not null)
                {
                    writer.WritePropertyName("numberFormat");
                    JsonSerializer.Serialize(writer, value.NumberFormat, options);
                }

                if (value.SandboxContent.HasValue)
                {
                    writer.WriteBoolean("sandboxContent", value.SandboxContent.Value);
                }

                if (value.SingleDescription.HasValue)
                {
                    writer.WriteBoolean("singleDescription", value.SingleDescription.Value);
                }

                if (value.TextColor is not null)
                {
                    writer.WriteString("textColor", value.TextColor);
                }

                if (value.Title is not null)
                {
                    writer.WriteString("title", value.Title);
                }

                writer.WriteEndObject();
            }
        }
    }
}
