namespace AzureMapsControl.Components.Drawing
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// An enumeration of the available drawing modes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class DrawingMode
    {
        private readonly string _mode;

        /// <summary>
        /// Draw individual points on the map.
        /// </summary>
        public static readonly DrawingMode DrawPoint = new DrawingMode("draw-point");

        /// <summary>
        /// Draw lines on the map.
        /// </summary>
        public static readonly DrawingMode DrawLine = new DrawingMode("draw-line");

        /// <summary>
        /// Draw polygons on the map.
        /// </summary>
        public static readonly DrawingMode DrawPolygon = new DrawingMode("draw-polygon");

        /// <summary>
        /// Draw circles on the map.
        /// </summary>
        public static readonly DrawingMode DrawCircle = new DrawingMode("draw-circle");

        /// <summary>
        /// Draw rectangles on the map.
        /// </summary>
        public static readonly DrawingMode DrawRectangle = new DrawingMode("draw-rectangle");

        /// <summary>
        /// When in this mode the user can add/remove/move points/coordinates of a shape, rotate shapes, drag shapes.
        /// </summary>
        public static readonly DrawingMode EditGeometry = new DrawingMode("edit-geometry");

        /// <summary>
        /// Sets the drawing manager into an idle state.
        /// Completes any drawing/edit that are in progress.
        /// </summary>
        public static readonly DrawingMode Idle = new DrawingMode("idle");

        private DrawingMode(string mode) => _mode = mode;

        public override string ToString() => _mode;
    }
}
