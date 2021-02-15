namespace AzureMapsControl.Components.Controls
{
    /// <summary>
    /// The options for a PitchControl object.
    /// </summary>
    public sealed class PitchControlOptions : ControlOptions
    {
        /// <summary>
        /// The angle that the map will tilt with each click of the control.
        /// </summary>
        public double? PitchDegreesDelta { get; set; }

        /// <summary>
        /// The style of the control.
        /// </summary>
        public ControlStyle Style { get; set; }
    }
}
