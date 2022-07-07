﻿namespace AzureMapsControl.Components.Tests.Json
{
    using System;
    using System.Buffers;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using Xunit;

    public abstract class JsonConverterTests<TValue>
    {
        private readonly JsonConverter<TValue> _converter;

        public JsonConverterTests(JsonConverter<TValue> converter) => _converter = converter;

        protected void TestAndAssertWrite(TValue value, string expectedJson)
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            _converter.Write(writer, value, null);

            writer.Flush();

            var serializedBytes = buffer.WrittenSpan.ToArray();

            var restored = Encoding.UTF8.GetString(serializedBytes);
            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(serializedBytes);

            var haveSameLength = expectedBytes.Length == buffer.WrittenCount;
            Assert.True(haveSameLength, userMessage: $"Different length detected, expected:{Environment.NewLine}'{expectedJson}'{Environment.NewLine}Actual:{Environment.NewLine}'{restored}'");
            Assert.Subset(expectedBytesSet, writterSet);
        }

        protected void TestAndAssertEmptyWrite(TValue value)
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            _converter.Write(writer, value, null);

            writer.Flush();

            Assert.Equal(0, buffer.WrittenCount);
        }

        protected TValue Read(string expectedJson)
        {
            var bytes = Encoding.UTF8.GetBytes(expectedJson);
            var reader = new Utf8JsonReader(bytes);

            var result = _converter.Read(ref reader, typeof(TValue), null);
            return result;
        }
    }
}
