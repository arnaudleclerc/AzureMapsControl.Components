﻿namespace AzureMapsControl.Components.Traffic
{
    /// <summary>
    /// Type of traffic flow to display
    /// </summary>

    public struct TrafficFlow
    {
        private readonly string _flow;

        /// <summary>
        /// Absolute speed of the road
        /// </summary>
        public static readonly TrafficFlow Absolute = new TrafficFlow("absolute");
        /// <summary>
        /// Display no traffic flow data
        /// </summary>
        public static readonly TrafficFlow None = new TrafficFlow("none");
        /// <summary>
        /// Speed of the road relative to free-flow
        /// </summary>
        public static readonly TrafficFlow Relative = new TrafficFlow("relative");
        /// <summary>
        /// Displays relative speed only where they differ from free-flow
        /// </summary>
        public static readonly TrafficFlow RelativeDelay = new TrafficFlow("relative-delay");

        private TrafficFlow(string flow) => _flow = flow;

        internal static TrafficFlow FromString(string type)
        {
            return type switch {
                "absolute" => Absolute,
                "relative" => Relative,
                "relative-delay" => RelativeDelay,
                "none" => None,
                _ => default
            };
        }

        public override string ToString() => _flow;
    }
}
