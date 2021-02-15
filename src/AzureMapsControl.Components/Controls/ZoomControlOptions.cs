namespace AzureMapsControl.Components.Controls
{
    public sealed class ZoomControlOptions : ControlOptions
    {
        /// <summary>
        /// The extent to which the map will zoom with each click of the control.
        /// </summary>
        public int? ZoomDelta { get; set; }

        /// <summary>
        /// The style of the control.
        /// </summary>
        public ControlStyle Style { get; set; }
    }
}
