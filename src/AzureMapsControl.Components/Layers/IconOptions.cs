namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used to customize the icons in a SymbolLayer
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class IconOptions
    {
        /// <summary>
        /// Specifies if the symbol icon can overlay other symbols on the map.
        /// </summary>
        public bool? AllowOverlap { get; set; }

        /// <summary>
        /// Specifies which part of the icon is placed closest to the icons anchor position on the map.
        /// </summary>
        public ExpressionOrString Anchor { get; set; }

        /// <summary>
        /// Specifies if other symbols can overlap this symbol.
        /// </summary>
        public bool? IgnorePlacement { get; set; }

        /// <summary>
        /// The name of the image in the map's image sprite to use for drawing the icon.
        /// </summary>
        public ExpressionOrString Image { get; set; }

        /// <summary>
        /// Specifies an offset distance of the icon from its anchor in pixels.
        /// </summary>
        public Expression Offset { get; set; }

        /// <summary>
        /// Specifies if a symbols icon can be hidden but its text displayed if it is overlapped with another symbol.
        /// </summary>
        public bool? Optional { get; set; }

        /// <summary>
        /// Specifies the orientation of the icon when the map is pitched.
        /// </summary>
        public PitchAlignment PitchAlignment { get; set; }

        /// <summary>
        /// The amount to rotate the icon clockwise in degrees
        /// </summary>
        public ExpressionOrNumber Rotation { get; set; }

        /// <summary>
        /// In combination with the placement property of a SymbolLayerOptions this determines the rotation behavior of icons.
        /// </summary>
        public PitchAlignment RotationAlignment { get; set; }

        /// <summary>
        /// Scales the original size of the icon by the provided factor.
        /// </summary>
        public ExpressionOrNumber Size { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the icon will be drawn.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }
    }
}
