namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(CameraTypeJsonConverter))]
    public sealed class CameraType
    {
        private readonly string _type;

        public static readonly CameraType Ease = new CameraType("ease");
        public static readonly CameraType Fly = new CameraType("fly");
        public static readonly CameraType Jump = new CameraType("jump");

        private CameraType(string type) => _type = type;

        public override string ToString() => _type;

        internal static CameraType FromString(string type)
        {
            switch (type)
            {
                case "ease":
                    return Ease;
                case "fly":
                    return Fly;
                default:
                    return Jump;
            }
        }
    }

    internal sealed class CameraTypeJsonConverter : JsonConverter<CameraType>
    {
        public override CameraType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => CameraType.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, CameraType value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
