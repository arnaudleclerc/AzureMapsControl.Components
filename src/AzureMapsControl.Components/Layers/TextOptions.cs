namespace AzureMapsControl.Components.Layers
{
    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used to customize the text in a SymbolLayer
    /// </summary>
    public sealed class TextOptions
    {
        /// <summary>
        /// Specifies if the text will be visible if it collides with other symbols.
        /// </summary>
        public bool? AllowOverlap { get; set; }

        /// <summary>
        /// Specifies which part of the icon is placed closest to the icons anchor position on the map.
        /// </summary>
        public ExpressionOrString Anchor { get; set; }

        /// <summary>
        /// Specifies the name of a property on the features to use for a text label.
        /// </summary>
        public ExpressionOrString TextField { get; set; }

        /// <summary>
        /// The font stack to use for displaying text.
        /// </summary>
        public ExpressionOrStringArray Font { get; set; }

        /// <summary>
        /// Specifies if the other symbols are allowed to collide with the text.
        /// </summary>
        public bool? IgnorePlacement { get; set; }

        /// <summary>
        /// Specifies an offset distance of the icon from its anchor in ems.
        /// </summary>
        public Expression Offset { get; set; }

        /// <summary>
        /// Specifies if the text can be hidden if it is overlapped by another symbol.
        /// </summary>
        public bool? Optional { get; set; }

        /// <summary>
        /// Specifies the orientation of the text when the map is pitched.
        /// </summary>
        public PitchAlignment PitchAlignment { get; set; }

        /// <summary>
        /// The amount to rotate the text clockwise in degrees.
        /// </summary>
        public ExpressionOrNumber Rotation { get; set; }

        /// <summary>
        /// In combination with the `placement` property of the `SymbolLayerOptions`, specifies the rotation behavior of the individual glyphs forming the text.
        /// </summary>
        public PitchAlignment RotationAlignment { get; set; }

        /// <summary>
        /// The size of the font in pixels.
        /// </summary>
        public ExpressionOrNumber Size { get; set; }

        /// <summary>
        /// The color of the text.
        /// </summary>
        public ExpressionOrString Color { get; set; }

        /// <summary>
        /// The halo's fadeout distance towards the outside in pixels.
        /// </summary>
        public ExpressionOrNumber HaloBlur { get; set; }

        /// <summary>
        /// The color of the text's halo, which helps it stand out from backgrounds.
        /// </summary>
        public ExpressionOrString HaloColor { get; set; }

        /// <summary>
        /// The distance of the halo to the font outline in pixels.
        /// </summary>
        public ExpressionOrNumber HaloWidth { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the text will be drawn.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }
    }
}
