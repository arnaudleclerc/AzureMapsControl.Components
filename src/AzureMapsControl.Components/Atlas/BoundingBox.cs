namespace AzureMapsControl.Atlas
{
    public class BoundingBox
    {
        /// <summary>
        /// The east edge of the bounding box.
        /// </summary>
        public double East { get; set; }

        /// <summary>
        /// The north edge of the bounding box.
        /// </summary>
        public double North { get; set; }

        /// <summary>
        /// The south edge of the bounding box.
        /// </summary>
        public double South { get; set; }

        /// <summary>
        /// The west edge of the bounding box.
        /// </summary>
        public double West { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="west">The west edge of the bounding box.</param>
        /// <param name="south">The south edge of the bounding box.</param>
        /// <param name="east">The east edge of the bounding box.</param>
        /// <param name="north">The north edge of the bounding box.</param>
        public BoundingBox(double west, double south, double east, double north)
        {
            West = west;
            South = south;
            East = east;
            North = north;
        }
    }
}
