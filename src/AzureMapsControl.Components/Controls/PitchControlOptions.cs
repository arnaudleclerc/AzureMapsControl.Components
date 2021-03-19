namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The options for a PitchControl object.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PitchControlOptions : IControlOptions
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
