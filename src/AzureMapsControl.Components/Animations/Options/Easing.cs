namespace AzureMapsControl.Components.Animations.Options
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(EasingJsonConverter))]
    public struct Easing
    {
        private readonly string _easing;

        /// <summary>
        /// Linear easing function.
        /// </summary>
        public static readonly Easing Linear = new("linear");

        /// <summary>
        /// Slight acceleration from zero to full speed.
        /// </summary>
        public static readonly Easing EaseInSine = new("easeInSine");

        /// <summary>
        /// Slight deceleration at the end.
        /// </summary>
        public static readonly Easing EaseOutSine = new("easeOutSine");

        /// <summary>
        /// Slight acceleration at beginning and slight deceleration at end.
        /// </summary>
        public static readonly Easing EaseInOutSine = new("easeInOutSine");

        /// <summary>
        /// Accelerating from zero velocity.
        /// </summary>
        public static readonly Easing EaseInQuad = new("easeInQuad");

        /// <summary>
        /// Decelerating to zero velocity.
        /// </summary>
        public static readonly Easing EaseOutQuad = new("easeOutQuad");

        /// <summary>
        /// Acceleration until halfway, then deceleration.
        /// </summary>
        public static readonly Easing EaseInOutQuad = new("easeInOutQuad");

        /// <summary>
        /// Accelerating from zero velocity.
        /// </summary>
        public static readonly Easing EaseInCubic = new("easeInCubic");

        /// <summary>
        /// Decelerating to zero velocity.
        /// </summary>
        public static readonly Easing EaseOutCubic = new("easeOutCubic");

        /// <summary>
        /// Acceleration until halfway, then deceleration.
        /// </summary>
        public static readonly Easing EaseInOutCubic = new("easeInOutCubic");

        /// <summary>
        /// Accelerating from zero velocity.
        /// </summary>
        public static readonly Easing EaseInQuart = new("easeInQuart");

        /// <summary>
        /// Decelerating to zero velocity.
        /// </summary>
        public static readonly Easing EaseOutQuart = new("easeOutQuart");

        /// <summary>
        /// Acceleration until halfway, then deceleration.
        /// </summary>
        public static readonly Easing EaseInOutQuart = new("easeInOutQuart");

        /// <summary>
        /// Accelerating from zero velocity.
        /// </summary>
        public static readonly Easing EaseInQuint = new("easeInQuint");

        /// <summary>
        /// Decelerating to zero velocity.
        /// </summary>
        public static readonly Easing EaseOutQuint = new("easeOutQuint");

        /// <summary>
        /// Acceleration until halfway, then deceleration.
        /// </summary>
        public static readonly Easing EaseInOutQuint = new("easeInOutQuint");

        /// <summary>
        /// Accelerate exponentially until finish.
        /// </summary>
        public static readonly Easing EaseInExpo = new("easeInExpo");

        /// <summary>
        /// Initial exponential acceleration slowing to stop.
        /// </summary>
        public static readonly Easing EaseOutExpo = new("easeOutExpo");

        /// <summary>
        /// Exponential acceleration and deceleration.
        /// </summary>
        public static readonly Easing EaseInOutExpo = new("easeInOutExpo");

        /// <summary>
        /// Increasing velocity until stop.
        /// </summary>
        public static readonly Easing EaseInCirc = new("easeInCirc");

        /// <summary>
        /// Start fast, decreasing velocity until stop.
        /// </summary>
        public static readonly Easing EaseOutCirc = new("easeOutCirc");

        /// <summary>
        /// Fast increase in velocity, fast decrease in velocity.
        /// </summary>
        public static readonly Easing EaseInOutCirc = new("easeInOutCirc");

        /// <summary>
        /// Slow movement backwards then fast snap to finish.
        /// </summary>
        public static readonly Easing EaseInBack = new("easeInBack");

        /// <summary>
        /// Fast snap to backwards point then slow resolve to finish.
        /// </summary>
        public static readonly Easing EaseOutBack = new("easeOutBack");

        /// <summary>
        /// Slow movement backwards, fast snap to past finish, slow resolve to finish.
        /// </summary>
        public static readonly Easing EaseInOutBack = new("easeInOutBack");

        /// <summary>
        /// Bounces slowly then quickly to finish.
        /// </summary>
        public static readonly Easing EaseInElastic = new("easeInElastic");

        /// <summary>
        /// Fast acceleration, bounces to zero.
        /// </summary>
        public static readonly Easing EaseOutElastic = new("easeOutElastic");

        /// <summary>
        /// Slow start and end, two bounces sandwich a fast motion.
        /// </summary>
        public static readonly Easing EaseInOutElastic = new("easeInOutElastic");

        /// <summary>
        /// Bounce increasing in velocity until completion
        /// </summary>
        public static readonly Easing EaseInBounce = new("easeInBounce");

        /// <summary>
        /// Bounce to completion.
        /// </summary>
        public static readonly Easing EaseOutBounce = new("easeOutBounce");

        /// <summary>
        /// Bounce in and bounce out.
        /// </summary>
        public static readonly Easing EaseInOutBounce = new("easeInOutBounce");

        private Easing(string easing) => _easing = easing;

        public override string ToString() => _easing;

        internal static Easing FromString(string easing)
        {
            return easing switch {
                "linear" => Linear,
                "easeInSine" => EaseInSine,
                "easeOutSine" => EaseOutSine,
                "easeInOutSine" => EaseInOutSine,
                "easeInQuad" => EaseInQuad,
                "easeOutQuad" => EaseOutQuad,
                "easeInOutQuad" => EaseInOutQuad,
                "easeInCubic" => EaseInCubic,
                "easeOutCubic" => EaseOutCubic,
                "easeInOutCubic" => EaseInOutCubic,
                "easeInQuart" => EaseInQuart,
                "easeOutQuart" => EaseOutQuart,
                "easeInOutQuart" => EaseInOutQuart,
                "easeInQuint" => EaseInQuint,
                "easeOutQuint" => EaseOutQuint,
                "easeInOutQuint" => EaseInOutQuint,
                "easeInExpo" => EaseInExpo,
                "easeOutExpo" => EaseOutExpo,
                "easeInOutExpo" => EaseInOutExpo,
                "easeInCirc" => EaseInCirc,
                "easeOutCirc" => EaseOutCirc,
                "easeInOutCirc" => EaseInOutCirc,
                "easeInBack" => EaseInBack,
                "easeOutBack" => EaseOutBack,
                "easeInOutBack" => EaseInOutBack,
                "easeInElastic" => EaseInElastic,
                "easeOutElastic" => EaseOutElastic,
                "easeInOutElastic" => EaseInOutElastic,
                "easeInBounce" => EaseInBounce,
                "easeOutBounce" => EaseOutBounce,
                "easeInOutBounce" => EaseInOutBounce,
                _ => default,
            };
        }
    }

    internal class EasingJsonConverter : JsonConverter<Easing>
    {
        public override Easing Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Easing.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, Easing value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
