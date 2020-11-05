namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(ExpressionJsonConverter))]
    public class Expression
    {
        public IEnumerable<Expression> Expressions { get; set; }

        internal Expression() { }

        public Expression(IEnumerable<Expression> expressions) => Expressions = expressions;
    }

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrNumberJsonConverter))]
    public sealed class ExpressionOrNumber : Expression
    {
        internal double? Value { get; }

        public ExpressionOrNumber(IEnumerable<Expression> expressions) => Expressions = expressions;

        /// <summary>
        /// Creates an expression
        /// </summary>
        /// <param name="value">Value which will be used instead of the expression</param>
        public ExpressionOrNumber(double? value) => Value = value;
    }

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrStringJsonConverter))]
    public sealed class ExpressionOrString : Expression
    {
        internal string Value { get; }

        public ExpressionOrString(IEnumerable<Expression> expressions) => Expressions = expressions;

        /// <summary>
        /// Creates an expression with the JSON Array to use
        /// </summary>
        /// <param name="expression">JSON Array representing the expression</param>
        public ExpressionOrString(string expression) => Value = expression;
    }

    internal abstract class ExpressionBaseJsonConverter<T> : JsonConverter<T> where T : Expression
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) => WriteExpression(writer, value, options);

        private void WriteExpression(Utf8JsonWriter writer, Expression value, JsonSerializerOptions options)
        {
            if (value.Expressions != null)
            {
                writer.WriteStartArray();
                foreach (var expression in value.Expressions)
                {
                    WriteExpression(writer, expression, options);
                }
                writer.WriteEndArray();
            }
            else if (value is ExpressionOrNumber)
            {
                WriteExpressionOrNumber(writer, value as ExpressionOrNumber);
            }
            else if (value is ExpressionOrString)
            {
                WriteExpressionOrString(writer, value as ExpressionOrString);
            }
        }

        private void WriteExpressionOrNumber(Utf8JsonWriter writer, ExpressionOrNumber value)
        {
            if (value.Value.HasValue)
            {
                writer.WriteNumberValue(value.Value.Value);
            }
        }

        private void WriteExpressionOrString(Utf8JsonWriter writer, ExpressionOrString value) => writer.WriteStringValue(value.Value);
    }

    internal class ExpressionJsonConverter : ExpressionBaseJsonConverter<Expression>
    {
    }

    internal class ExpressionOrNumberJsonConverter : ExpressionBaseJsonConverter<ExpressionOrNumber>
    {
    }

    internal class ExpressionOrStringJsonConverter : ExpressionBaseJsonConverter<ExpressionOrString>
    {
    }
}
