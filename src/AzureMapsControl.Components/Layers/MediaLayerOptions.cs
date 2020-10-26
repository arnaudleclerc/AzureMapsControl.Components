namespace AzureMapsControl.Components.Layers
{
    /// <summary>
    /// Options used when rendering canvas, image, raster tile, and video layers
    /// </summary>
    public abstract class MediaLayerOptions : LayerOptions
    {
        /// <summary>
        /// A number between -1 and 1 that increases or decreases the contrast of the overlay.
        /// </summary>
        public int? Contrast { get; set; }

        /// <summary>
        /// The duration in milliseconds of a fade transition when a new tile is added.
        /// Must be greater or equal to 0.
        /// </summary>
        public int? FadeDuration { get; set; }

        /// <summary>
        /// Rotates hues around the color wheel.
        /// A number in degrees.
        /// </summary>
        public int? HueRotation { get; set; }

        /// <summary>
        /// A number between 0 and 1 that increases or decreases the maximum brightness of the overlay.
        /// </summary>
        public int? MaxBrightness { get; set; }

        /// <summary>
        /// A number between 0 and 1 that increases or decreases the minimum brightness of the overlay.
        /// </summary>
        public int? MinBrightness { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the overlay will be drawn.
        /// </summary>
        public int? Opacity { get; set; }

        /// <summary>
        /// A number between -1 and 1 that increases or decreases the saturation of the overlay.
        /// </summary>
        public int? Saturation { get; set; }
    }
}
