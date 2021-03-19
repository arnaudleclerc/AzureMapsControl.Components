namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class ScaleBarControlOptions : IControlOptions
    {
        /// <summary>
        /// The maximum length of the scale bar in pixels
        /// </summary>
        public int? MaxBarLength { get; set; }

        /// <summary>
        /// The distance units of the scale bar.
        /// </summary>
        public ScaleBarControlUnits Units { get; set; }
    }
}
