namespace AzureMapsControl.Components.Controls
{
    public sealed class ScaleBarControlOptions : ControlOptions
    {
        /// <summary>
        /// The maximum length of the scale bar in pixels
        /// </summary>
        public int? MaxBarLength { get; set; }

        /// <summary>
        /// The distance units of the scale bar.
        /// </summary>
        public ScalebarControlUnits Units { get; set; }
    }
}
