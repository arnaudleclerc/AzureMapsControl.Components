namespace AzureMapsControl.Components.Drawing
{
    public sealed class DrawingButton
    {
        private readonly string _button;

        /// <summary>
        /// Draw individual points on the map.
        /// </summary>
        public static readonly DrawingButton DrawPoint = new DrawingButton(DrawingMode.DrawPoint.ToString());

        /// <summary>
        /// Draw lines on the map.
        /// </summary>
        public static readonly DrawingButton DrawLine = new DrawingButton(DrawingMode.DrawLine.ToString());

        /// <summary>
        /// Draw polygons on the map.
        /// </summary>
        public static readonly DrawingButton DrawPolygon = new DrawingButton(DrawingMode.DrawPolygon.ToString());

        /// <summary>
        /// Draw circles on the map.
        /// </summary>
        public static readonly DrawingButton DrawCircle = new DrawingButton(DrawingMode.DrawCircle.ToString());

        /// <summary>
        /// Draw rectangles on the map.
        /// </summary>
        public static readonly DrawingButton DrawRectangle = new DrawingButton(DrawingMode.DrawRectangle.ToString());

        /// <summary>
        /// When in this mode the user can add/remove/move points/coordinates of a shape, rotate shapes, drag shapes.
        /// </summary>
        public static readonly DrawingButton EditGeometry = new DrawingButton(DrawingMode.EditGeometry.ToString());

        private DrawingButton(string button) => _button = button;

        public override string ToString() => _button;
    }
}
