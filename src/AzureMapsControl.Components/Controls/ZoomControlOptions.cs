namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class ZoomControlOptions : IControlOptions
    {
        /// <summary>
        /// The extent to which the map will zoom with each click of the control.
        /// </summary>
        public double? ZoomDelta { get; set; }

        /// <summary>
        /// The style of the control.
        /// </summary>
        public ControlStyle Style { get; set; }
    }
}
