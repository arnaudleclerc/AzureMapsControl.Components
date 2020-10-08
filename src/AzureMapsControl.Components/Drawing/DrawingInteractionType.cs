namespace AzureMapsControl.Components.Drawing
{
    /// <summary>
    /// An enumeration of the available drawing interaction types.
    /// The drawing interaction type specifies how certain drawing modes behave.
    /// </summary>
    public sealed class DrawingInteractionType
    {
        private readonly string _interactionType;

        /// <summary>
        /// Coordinates are added when the mouse or touch is clicked or dragged.
        /// </summary>
        public static readonly DrawingInteractionType Hybrid = new DrawingInteractionType("hybrid");

        /// <summary>
        /// Coordinates are added when the mouse or touch is dragged on the map.
        /// </summary>
        public static readonly DrawingInteractionType Freehand = new DrawingInteractionType("freehand");

        /// <summary>
        /// Coordinates are added when the mouse or touch is clicked.
        /// </summary>
        public static readonly DrawingInteractionType Click = new DrawingInteractionType("click");

        private DrawingInteractionType(string interactionType) => _interactionType = interactionType;

        public override string ToString() => _interactionType;
    }
}
