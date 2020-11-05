namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrNumberJsonConverter))]
    public sealed class ExpressionOrNumber
    {
        internal string StringValue { get; }
        internal double? DoubleValue { get; }

        /// <summary>
        /// Creates an expression with the JSON Array to use
        /// </summary>
        /// <param name="expression">JSON Array representing the expression</param>
        public ExpressionOrNumber(string expression) => StringValue = expression;

        /// <summary>
        /// Creates an expression
        /// </summary>
        /// <param name="value">Value which will be used instead of the expression</param>
        public ExpressionOrNumber(double? value) => DoubleValue = value;
    }

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrStringJsonConverter))]
    public sealed class ExpressionOrString
    {
        internal string Value { get; }

        /// <summary>
        /// Creates an expression with the JSON Array to use
        /// </summary>
        /// <param name="expression">JSON Array representing the expression</param>
        public ExpressionOrString(string expression) => Value = expression;
    }

    internal class ExpressionOrNumberJsonConverter : JsonConverter<ExpressionOrNumber>
    {
        public override ExpressionOrNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new ExpressionOrNumber(reader.GetString());

        public override void Write(Utf8JsonWriter writer, ExpressionOrNumber value, JsonSerializerOptions options)
        {
            if (value.DoubleValue.HasValue)
            {
                writer.WriteNumberValue(value.DoubleValue.Value);
            }
            else
            {
                writer.WriteStringValue(value.StringValue);
            }
        }
    }

    internal class ExpressionOrStringJsonConverter : JsonConverter<ExpressionOrString>
    {
        public override ExpressionOrString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new ExpressionOrString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, ExpressionOrString value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
    }
}
