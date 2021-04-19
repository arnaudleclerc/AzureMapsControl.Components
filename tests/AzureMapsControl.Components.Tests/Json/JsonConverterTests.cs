namespace AzureMapsControl.Components.Tests.Json
{
    using System.Buffers;
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

            var expectedBytes = Encoding.UTF8.GetBytes(expectedJson);

            var expectedBytesSet = new System.Collections.Generic.HashSet<byte>(expectedBytes);
            var writterSet = new System.Collections.Generic.HashSet<byte>(buffer.WrittenSpan.ToArray());

            Assert.Equal(expectedBytes.Length, buffer.WrittenCount);
            Assert.Subset(expectedBytesSet, writterSet);
        }

        protected void TestAndAssertEmptytWrite(TValue value)
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);

            _converter.Write(writer, value, null);

            writer.Flush();

            Assert.Equal(0, buffer.WrittenCount);
        }
    }
}
