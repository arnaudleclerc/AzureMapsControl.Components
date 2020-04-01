namespace AzureMapsControl.Atlas
{
    public class Position
    {
        /// <summary>
        /// The position's elevation.
        /// </summary>
        public int? Elevation { get; set; }

        /// <summary>
        /// The position's latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The position's longitude.
        /// </summary>
        public double Longitude { get; set; }

        public Position() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="longitude">The position's longitude.</param>
        /// <param name="latitude">The position's latitude.</param>
        public Position(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="longitude">The position's longitude.</param>
        /// <param name="latitude">The position's latitude.</param>
        /// <param name="elevation">The position's elevation.</param>
        public Position(double longitude, double latitude, int elevation) : this(longitude, latitude) => Elevation = elevation;
    }
}
