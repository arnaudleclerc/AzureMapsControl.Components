namespace AzureMapsControl.Components.Atlas
{
    public sealed class PitchAlignment
    {
        private readonly string _type;

        /// <summary>
        /// The circle is aligned to the plane of the map.
        /// </summary>
        public static PitchAlignment Map = new PitchAlignment("map");

        /// <summary>
        /// The circle is aligned to the plane of the viewport
        /// </summary>
        public static PitchAlignment ViewPort = new PitchAlignment("viewport");

        private PitchAlignment(string type) => _type = type;

        public override string ToString() => _type;

        internal static PitchAlignment FromString(string type)
        {
            if (Map.ToString() == type)
            {
                return Map;
            }

            return ViewPort.ToString() == type ? ViewPort : null;
        }
    }
}
