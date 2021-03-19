namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(ControlJsonConverter))]
    [ExcludeFromCodeCoverage]
    public abstract class Control
    {
        internal abstract string Type { get; }
        internal abstract int Order { get; }
        internal Guid Id { get; }

        /// <summary>
        /// Position of the control
        /// </summary>
        public ControlPosition Position { get; }

        private Control() => Id = Guid.NewGuid();

        internal Control(ControlPosition position) : this() => Position = position;
    }

    [ExcludeFromCodeCoverage]
    public abstract class Control<T> : Control
        where T : IControlOptions
    {
        /// <summary>
        /// Options of the control
        /// </summary>
        protected internal T Options { get; protected set; }

        internal Control(T options, ControlPosition position) : base(position) => Options = options;
    }

    internal class ControlJsonConverter : JsonConverter<Control>
    {
        public override Control Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, Control value, JsonSerializerOptions options)
        {
            switch (value.Type)
            {
                case "compass":
                    CompassControlJsonConverter.Write(writer, value as CompassControl);
                    break;
                case "pitch":
                    PitchControlJsonConverter.Write(writer, value as PitchControl);
                    break;
                case "style":
                    StyleControlJsonConverter.Write(writer, value as StyleControl);
                    break;
                case "zoom":
                    ZoomControlJsonConverter.Write(writer, value as ZoomControl);
                    break;
                case "scalebar":
                    ScaleBarControlJsonConverter.Write(writer, value as ScaleBarControl);
                    break;
                case "overviewmap":
                    OverviewMapControlJsonConverter.Write(writer, value as OverviewMapControl);
                    break;
            }
        }
    }
}
