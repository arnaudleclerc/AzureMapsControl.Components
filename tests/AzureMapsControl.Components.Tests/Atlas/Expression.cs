namespace AzureMapsControl.Components.Tests.Atlas
{
    using System;
    using System.Diagnostics;
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

        [Fact]
        public void GetProperty_WhenCalled_ProducesCompliantJson()
        {
            var propertyName = "Confirmed";
            var expression = Expression.GetProperty(propertyName);
            TestAndAssertWrite(expression, @$"[""get"",""{propertyName}""]");
        }

        [Fact]
        public void HasProperty_WhenCalled_ProducesCompliantJson()
        {
            var propertyName = "Confirmed";
            var expression = Expression.HasProperty(propertyName);
            TestAndAssertWrite(expression, @$"[""has"",""{propertyName}""]");
        }

        [Fact]
        public void IsCluster_WhenCalled_ProducesCompliantJson()
            => TestAndAssertWrite(Expression.IsCluster, expectedJson: @"[""has"",""point_count""]");

        [Fact]
        public void Conditional_WhenCalled_ProducesCompliantJson()
        {
            var clusterProp = Expression.GetProperty("clusterValue");
            var leafProp = Expression.GetProperty("leafValue");
            var expression = Expression.Conditional(Expression.IsCluster, clusterProp, leafProp);
            TestAndAssertWrite(expression, @"[""case"",[""has"",""point_count""],[""get"",""clusterValue""],[""get"",""leafValue""]]");
        }

        [Fact]
        public void ToNumber_WhenNotYetNumber_Wraps()
        {
            var propGetter = Expression.GetProperty("iAmNumber");
            var expression = propGetter.ToNumber();
            TestAndAssertWrite(expression, @"[""to-number"",[""get"",""iAmNumber""]]");
        }
    }

    public class ExpressionOrNumberTests
    {
        [Fact]
        public void Number_WhenSupplied_CanBeExpressionWithoutBoilerplate()
        {
            var radius = Expression.Conditional(Expression.IsCluster, new ExpressionOrNumber(10), new ExpressionOrNumber(5));
            Assert.IsType<Expression>(radius);
        }

        [Fact]
        public void Type_Is_DebugFriendly()
        {
            var attributes = typeof(ExpressionOrNumber).GetCustomAttributesData();
            Assert.Contains(attributes, attribute => attribute.AttributeType == typeof(DebuggerDisplayAttribute));
        }

        [Fact]
        public void ToNumber_WhenAlreadyNumber_ReturnsSelf()
        {
            Expression alreadyNumber = new ExpressionOrNumber(5);
            var expression = alreadyNumber.ToNumber();
            Assert.Same(alreadyNumber, expression);
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

    public class ExpressionOrStringTests
    {
        [Fact]
        public void Type_Is_DebugFriendly()
        {
            var attributes = typeof(ExpressionOrString).GetCustomAttributesData();
            Assert.Contains(attributes, attribute => attribute.AttributeType == typeof(DebuggerDisplayAttribute));
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
