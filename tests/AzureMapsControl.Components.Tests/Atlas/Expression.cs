namespace AzureMapsControl.Components.Tests.Atlas
{
    using System;
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class ExpressionJsonConverterTests : JsonConverterTests<Expression>
    {
        public ExpressionJsonConverterTests() : base(new ExpressionJsonConverter()) { }

        [Fact]
        public void Should_WriteJson()
        {
            var expectedJson = "[\"get\",\"Confirmed\"]";
            var expression = new Expression(JsonDocument.Parse(expectedJson));

            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new Expression(new[] { child });

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            TestAndAssertWrite(expression, expectedJson);
        }
    }

    public class ExpressionOrNumberJsonConverterTests : JsonConverterTests<ExpressionOrNumber>
    {
        public ExpressionOrNumberJsonConverterTests() : base(new ExpressionOrNumberJsonConverter()) { }

        [Fact]
        public void Should_WriteJson()
        {
            var expectedJson = "[\"get\",\"Confirmed\"]";
            var expression = new ExpressionOrNumber(JsonDocument.Parse(expectedJson));

            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new ExpressionOrNumber(new[] { child });

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteNumberValue()
        {
            var value = 1;
            var expression = new ExpressionOrNumber(value);

            TestAndAssertWrite(expression, value.ToString());
        }

        [Fact]
        public void Should_NotWriteNumberValue()
        {
            double? value = null;
            var expression = new ExpressionOrNumber(value);

            TestAndAssertEmptyWrite(expression);
        }
    }

    public class ExpressionOrStringJsonConverterTests : JsonConverterTests<ExpressionOrString>
    {
        public ExpressionOrStringJsonConverterTests() : base(new ExpressionOrStringJsonConverter()) { }


        [Fact]
        public void Should_WriteJson()
        {
            var expectedJson = "[\"get\",\"Confirmed\"]";
            var expression = new ExpressionOrString(JsonDocument.Parse(expectedJson));

            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new ExpressionOrString(new[] { child });

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteStringValue()
        {
            var value = "value";
            var expression = new ExpressionOrString(value);

            var expectedJson = "\"" + value.ToString() + "\"";
            TestAndAssertWrite(expression, expectedJson);
        }
    }

    public class ExpressionOrStringArrayConverterTests : JsonConverterTests<ExpressionOrStringArray>
    {
        public ExpressionOrStringArrayConverterTests() : base(new ExpressionOrStringArrayJsonConverter()) { }

        [Fact]
        public void Should_WriteJson()
        {
            var expectedJson = "[\"get\",\"Confirmed\"]";
            var expression = new ExpressionOrStringArray(JsonDocument.Parse(expectedJson));

            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new ExpressionOrStringArray(new[] { child });

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_WriteStringArray()
        {
            var value = "value";
            var expression = new ExpressionOrStringArray(new[] { value });

            var expectedJson = "[\"" + value.ToString() + "\"]";
            TestAndAssertWrite(expression, expectedJson);
        }

        [Fact]
        public void Should_NotWriteStringArray_NullCase()
        {
            string[] values = null;
            var expression = new ExpressionOrStringArray(values);

            TestAndAssertEmptyWrite(expression);
        }

        [Fact]
        public void Should_WriteStringArray_EmptyCase()
        {
            var expression = new ExpressionOrStringArray(Array.Empty<string>());

            var expectedJson = "[]";
            TestAndAssertWrite(expression, expectedJson);
        }
    }
}
