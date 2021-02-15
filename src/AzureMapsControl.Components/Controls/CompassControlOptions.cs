namespace AzureMapsControl.Components.Controls
{
    /// <summary>
    /// The options for a CompassControl object.
    /// </summary>
    public sealed class CompassControlOptions : ControlOptions
    {
        /// <summary>
        /// The angle that the map will rotate with each click of the control.
        /// </summary>
        public decimal? RotationDegreesDelta { get; set; }

        /// <summary>
        /// The style of the control.
        /// </summary>
        public ControlStyle Style { get; set; }
    }
}
