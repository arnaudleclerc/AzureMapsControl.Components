namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control that toggles the map or a specific container from its defined size to a fullscreen size.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FullScreenControl : Control<FullScreenControlOptions>
    {
        internal override string Type => "fullscreen";

        internal override int Order => 0;

        public FullScreenControl(FullScreenControlOptions options = null, ControlPosition position = default) : base(options, position) { }
    }

    internal class FullScreenControlJsonConverter : JsonConverter<FullScreenControl>
    {
        public override FullScreenControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, FullScreenControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, FullScreenControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WriteString("type", value.Type);
            if (value.Position.ToString() != default(ControlPosition).ToString())
            {
                writer.WriteString("position", value.Position.ToString());
            }
            if (value.Options is not null)
            {
                writer.WritePropertyName("options");
                writer.WriteStartObject();
                if (value.Options.Container is not null)
                {
                    writer.WriteString("container", value.Options.Container);
                }
                if (value.Options.HideIfUnsupported.HasValue)
                {
                    writer.WriteBoolean("hideIfUnsupported", value.Options.HideIfUnsupported.Value);
                }
                if (value.Options.Style is not null)
                {
                    writer.WriteString("style", value.Options.Style.HasFirstChoice ? value.Options.Style.FirstChoice.ToString() : value.Options.Style.SecondChoice);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
