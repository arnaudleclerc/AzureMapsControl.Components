namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    /**
     * Represent a pixel coordinate or offset.
     */
    [ExcludeFromCodeCoverage]
    public sealed class Pixel
    {
        /// <summary>
        /// The horizontal pixel offset
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The vertical pixel offset
        /// </summary>
        public double Y { get; set; }

        public Pixel() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">The horizontal pixel offset</param>
        /// <param name="y">The vertical pixel offset</param>
        public Pixel(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
