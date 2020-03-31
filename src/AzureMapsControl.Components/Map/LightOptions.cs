namespace AzureMapsControl.Map
{
    /// <summary>
    /// The options for the map's lighting.
    /// </summary>
    public class LightOptions
    {
        /// <summary>
        /// Specifies wether extruded geometries are lit relative to the map or viewport.
        /// "map": The position of the light source is aligned to the rotation of the map.
        /// "viewport": The position fo the light source is aligned to the rotation of the viewport.
        /// Default: "map"
        /// </summary>
        public LightAnchor Anchor { get; set; } = LightAnchor.Map;
        /// <summary>
        /// Color tint for lighting extruded geometries
        /// Default: "#FFFFFF"
        /// </summary>
        public string Color { get; set; } = "#FFFFFF";
        /// <summary>
        /// Intensity of lighting (on a scale from 0 to 1).
        /// Higher numbers will present as more extreme contrast.
        /// Default : 0.5
        /// </summary>
        public double Intensity { get; set; } = 0.5;
        /// <summary>
        /// Position of the light source relative to lit (extruded) geometries,
        /// in [r radial coordinate, a azimuthal angle, p polar angle]
        /// where r indicates the distance from the center of the base of an object to its light,
        /// a indicates the position of the light relative to 0°
        /// (0° when `anchor` is set to viewport corresponds to the top of the viewport,
        /// or 0° when `anchor` is set to map corresponds to due north, and degrees proceed clockwise),
        /// and p indicates the height of the light (from 0°, directly above, to 180°, directly below)
        /// </summary>
        public LightOptionsPosition Position { get; set; }
    }
}
