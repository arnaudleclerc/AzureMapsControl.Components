namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(CameraTypeJsonConverter))]
    public struct CameraType
    {
        private readonly string _type;

        public static readonly CameraType Ease = new("ease");
        public static readonly CameraType Fly = new("fly");
        public static readonly CameraType Jump = new("jump");

        private CameraType(string type) => _type = type;

        public override string ToString() => _type;

        /// <summary>
        /// Return a CameraType corresponding to the given value
        /// </summary>
        /// <param name="type">Value of the CameraType</param>
        /// <returns>CameraType corresponding to the given value. If none was found, returns `default`</returns>
        public static CameraType FromString(string type)
        {
            return type switch {
                "ease" => Ease,
                "fly" => Fly,
                "jump" => Jump,
                _ => default,
            };
        }
    }

    internal sealed class CameraTypeJsonConverter : JsonConverter<CameraType>
    {
        public override CameraType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => CameraType.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, CameraType value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
