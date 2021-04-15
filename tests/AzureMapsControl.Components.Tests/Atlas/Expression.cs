namespace AzureMapsControl.Components.Tests.Atlas
{
    using System;
    using System.Buffers;
    using System.Text;
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;

    using Xunit;

    public class ExpressionJsonConverterTests
    {
        [Fact]
        public void Should_WriteJson()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var expression = new Expression(JsonDocument.Parse(json));
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes(json);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new Expression(new[] { child });
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }
    }

    public class ExpressionOrNumberJsonConverterTests
    {
        [Fact]
        public void Should_WriteJson()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var expression = new ExpressionOrNumber(JsonDocument.Parse(json));
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrNumberJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes(json);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new ExpressionOrNumber(new[] { child });
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrNumberJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteNumberValue()
        {
            var value = 1;
            var expression = new ExpressionOrNumber(value);
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrNumberJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes(value.ToString());

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_NotWriteNumberValue()
        {
            double? value = null;
            var expression = new ExpressionOrNumber(value);
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrNumberJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            Assert.Equal(0, buffer.WrittenCount);
        }
    }

    public class ExpressionOrStringJsonConverterTests
    {
        [Fact]
        public void Should_WriteJson()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var expression = new ExpressionOrString(JsonDocument.Parse(json));
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes(json);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new ExpressionOrString(new[] { child });
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteStringValue()
        {
            var value = "value";
            var expression = new ExpressionOrString(value);
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes("\"" + value.ToString() + "\"");

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }
    }

    public class ExpressionOrStringArrayConverterTests
    {
        [Fact]
        public void Should_WriteJson()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var expression = new ExpressionOrStringArray(JsonDocument.Parse(json));
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringArrayJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes(json);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteExpressions()
        {
            var json = "[\"get\",\"Confirmed\"]";
            var child = new Expression(JsonDocument.Parse(json));
            var expression = new ExpressionOrStringArray(new[] { child });
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringArrayJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedJson = "[[\"get\",\"Confirmed\"]]";
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_WriteStringArray()
        {
            var value = "value";
            var expression = new ExpressionOrStringArray(new[] { value });
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringArrayJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes("[\"" + value.ToString() + "\"]");

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        [Fact]
        public void Should_NotWriteStringArray_NullCase()
        {
            string[] values = null;
            var expression = new ExpressionOrStringArray(values);
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringArrayJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            Assert.Equal(0, buffer.WrittenCount);
        }

        [Fact]
        public void Should_WriteStringArray_EmptyCase()
        {
            var expression = new ExpressionOrStringArray(Array.Empty<string>());
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            var converter = new ExpressionOrStringArrayJsonConverter();
            converter.Write(writer, expression, null);

            writer.Flush();

            var expectedBytes = Encoding.UTF8.GetBytes("[]");

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }
    }
}
