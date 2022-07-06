namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(ExpressionJsonConverter))]
    [ExcludeFromCodeCoverage]
    public class Expression
    {
        /// <summary>
        /// Json Document which will be used as the expression
        /// </summary>
        internal JsonDocument Json { get; }

        /// <summary>
        /// Expressions attached to this expression
        /// </summary>
        internal IEnumerable<Expression> Expressions { get; }

        internal Expression() { }

        /// <summary>
        /// Creates an expression.
        /// </summary>
        /// <param name="expressions">Expressions to include in this expression.</param>
        public Expression(IEnumerable<Expression> expressions) => Expressions = expressions;

        /// <summary>
        /// Creates an expression.
        /// </summary>
        /// <param name="json">Json representation of the expression.</param>
        public Expression(JsonDocument json) => Json = json;

        private static readonly Expression s_getter = new ExpressionOrString("get");

        /// <summary>
        /// An expression getting property value by <paramref name="propertyName"/>.
        /// <para>Cluster properties are supplied via <seealso cref="Data.DataSourceOptions.ClusterProperties"/>.</para>
        /// <para>Leaf level properties are supplied in data itself (f.e. <see cref="Feature.Properties"/>).</para>
        /// </summary>
        /// <param name="propertyName">The property name to get value.</param>
        /// <returns>An expression to fetch property value.</returns>
        public static Expression GetProperty(string propertyName) => new(new[] { s_getter, new ExpressionOrString(propertyName) });

        /// <summary>
        /// An expression checking if <paramref name="propertyName"/> is defined in node.
        /// <para>Typically used during data clustering to check if cluster node has property</para>
        /// <para>See <seealso cref="Data.DataSourceOptions.ClusterProperties"/>.</para>
        /// </summary>
        /// <param name="propertyName">The property name to check existance.</param>
        /// <returns>Expression that will evaluate into <c>true</c> if cluster has property; <c>false</c> otherwise.</returns>
        public static Expression HasProperty(string propertyName) => new(new[] { new ExpressionOrString("has"), new ExpressionOrString(propertyName) });

        /// <summary>
        /// An expression conditionally evaluating either <paramref name="ifTrue"/> or <paramref name="ifFalse"/> based on <paramref name="condition"/>.
        /// <para>Typically used during cluster/leaf property fetch, as cluster has only aggregated properties, while leaf level has more.</para>
        /// </summary>
        /// <param name="condition">The expression evaluating to <see cref="bool"/> (f.e. <see cref="IsCluster"/>).</param>
        /// <param name="ifTrue">The expression to evaluate if <paramref name="condition"/> was <c>true</c>.</param>
        /// <param name="ifFalse">The expression to evaluate if <paramref name="condition"/> was <c>false</c>.</param>
        /// <returns>An expression conditionally evaluating either expression based on <paramref name="condition"/>.</returns>
        public static Expression Conditional(Expression condition, Expression ifTrue, Expression ifFalse) => new(new[] { new ExpressionOrString("case"), condition, ifTrue, ifFalse });

        /// <summary>
        /// An expression checking if node is cluster, or leaf.
        /// <para>See <seealso cref="Data.DataSourceOptions.Cluster"/></para>
        /// </summary>
        public static readonly Expression IsCluster = HasProperty(ClusterProperties.PointCount);

        /// <summary>
        /// Holds cluster-specific properties provided by clustering engine, see <seealso cref="Data.DataSourceOptions.Cluster"/>.
        /// <para>
        ///     <seealso cref="https://docs.microsoft.com/en-us/azure/azure-maps/clustering-point-data-web-sdk"/>
        /// </para>
        /// </summary>
        private struct ClusterProperties
        {
            /// <summary>
            /// Point count exists only for cluster-level; leaf-level nodes do not have it.
            /// </summary>
            public static readonly string PointCount = "point_count";
        }
    }

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrNumberJsonConverter))]
    [ExcludeFromCodeCoverage]
    public sealed class ExpressionOrNumber : Expression
    {
        internal double? Value { get; }

        /// <summary>
        /// <inheritdoc cref="Expression(IEnumerable{Expression})"/>
        /// </summary>
        /// <param name="expressions"><inheritdoc/></param>
        public ExpressionOrNumber(IEnumerable<Expression> expressions) : base(expressions) { }

        /// <summary>
        /// <inheritdoc cref="Expression(JsonDocument)"/>
        /// </summary>
        /// <param name="json"><inheritdoc/></param>
        public ExpressionOrNumber(JsonDocument json) : base(json) { }

        /// <summary>
        /// Creates an expression.
        /// </summary>
        /// <param name="value">Value which will be used instead of the expression</param>
        public ExpressionOrNumber(double? value) => Value = value;
    }

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrStringJsonConverter))]
    [ExcludeFromCodeCoverage]
    public sealed class ExpressionOrString : Expression
    {
        internal string Value { get; }

        /// <summary>
        /// <inheritdoc cref="Expression(IEnumerable{Expression})"/>
        /// </summary>
        /// <param name="expressions"><inheritdoc/></param>
        public ExpressionOrString(IEnumerable<Expression> expressions) : base(expressions) { }

        /// <summary>
        /// <inheritdoc cref="Expression(JsonDocument)"/>
        /// </summary>
        /// <param name="json"><inheritdoc/></param>
        public ExpressionOrString(JsonDocument json) : base(json) { }

        /// <summary>
        /// Creates an expression with the JSON Array to use
        /// </summary>
        /// <param name="expression">JSON Array representing the expression</param>
        public ExpressionOrString(string expression) => Value = expression;
    }

    /// <summary>
    /// Can be specified as the value of filter or certain layer options.
    /// </summary>
    [JsonConverter(typeof(ExpressionOrStringArrayJsonConverter))]
    [ExcludeFromCodeCoverage]
    public sealed class ExpressionOrStringArray : Expression
    {
        internal IEnumerable<string> Values { get; }

        /// <summary>
        /// <inheritdoc cref="Expression(IEnumerable{Expression})"/>
        /// </summary>
        /// <param name="expressions"><inheritdoc/></param>
        public ExpressionOrStringArray(IEnumerable<Expression> expressions) : base(expressions) { }

        /// <summary>
        /// <inheritdoc cref="Expression(JsonDocument)"/>
        /// </summary>
        /// <param name="json"><inheritdoc/></param>
        public ExpressionOrStringArray(JsonDocument json): base(json) { }

        /// <summary>
        /// Creates an expression.
        /// </summary>
        /// <param name="values">Values of the expression</param>
        public ExpressionOrStringArray(IEnumerable<string> values) => Values = values;
    }

    internal abstract class ExpressionBaseJsonConverter<T> : JsonConverter<T> where T : Expression
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) => WriteExpression(writer, value, options);

        private void WriteExpression(Utf8JsonWriter writer, Expression value, JsonSerializerOptions options)
        {
            if(value.Json != null)
            {
                value.Json.WriteTo(writer);
            }
            else if (value.Expressions != null)
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
            else if (value is ExpressionOrStringArray)
            {
                WriteExpressionOrStringArray(writer, value as ExpressionOrStringArray);
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

        private void WriteExpressionOrStringArray(Utf8JsonWriter writer, ExpressionOrStringArray value)
        {
            if (value.Values is not null)
            {
                writer.WriteStartArray();
                foreach (var expression in value.Values)
                {
                    writer.WriteStringValue(expression);
                }
                writer.WriteEndArray();
            }
        }
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

    internal class ExpressionOrStringArrayJsonConverter : ExpressionBaseJsonConverter<ExpressionOrStringArray> { }
}
