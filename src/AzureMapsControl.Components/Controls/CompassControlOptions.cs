namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The options for a CompassControl object.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class CompassControlOptions : IControlOptions
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
