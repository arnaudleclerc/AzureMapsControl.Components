namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class OverviewMapControlOverlay
    {
        private readonly string _overlay;

        /// <summary>
        /// Shows a polygon area of the parent map view port.
        /// </summary>
        public static readonly OverviewMapControlOverlay Area = new OverviewMapControlOverlay("area");

        /// <summary>
        /// Shows a marker for the center of the parent map.
        /// </summary>
        public static readonly OverviewMapControlOverlay Marker = new OverviewMapControlOverlay("marker");

        /// <summary>
        /// Does not display any overlay on top of the overview map.
        /// </summary>
        public static readonly OverviewMapControlOverlay None = new OverviewMapControlOverlay("none");

        private OverviewMapControlOverlay(string overlay) => _overlay = overlay;

        public override string ToString() => _overlay;
    }
}
