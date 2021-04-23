namespace AzureMapsControl.Components.Atlas.FormatOptions
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Specifies the where the hyperlink should open.
    /// </summary>
    [JsonConverter(typeof(HyperLinkFormatOptionsTargetJsonConverter))]
    public struct HyperLinkFormatOptionsTarget
    {
        private readonly string _type;

        public static HyperLinkFormatOptionsTarget Blank = new HyperLinkFormatOptionsTarget("_blank");
        public static HyperLinkFormatOptionsTarget Self = new HyperLinkFormatOptionsTarget("_self");
        public static HyperLinkFormatOptionsTarget Parent = new HyperLinkFormatOptionsTarget("_parent");
        public static HyperLinkFormatOptionsTarget Top = new HyperLinkFormatOptionsTarget("_top");

        private HyperLinkFormatOptionsTarget(string type) => _type = type;

        public override string ToString() => _type;
    }

    internal class HyperLinkFormatOptionsTargetJsonConverter : JsonConverter<HyperLinkFormatOptionsTarget>
    {
        public override HyperLinkFormatOptionsTarget Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, HyperLinkFormatOptionsTarget value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
