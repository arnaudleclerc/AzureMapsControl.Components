namespace AzureMapsControl.Components.Controls
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Map;

    /// <summary>
    /// The options for a StyleControl object.
    /// </summary>
    public sealed class StyleControlOptions : ControlOptions
    {
        /// <summary>
        /// The layout to display the styles in.
        /// </summary>
        public StyleControlLayout StyleControlLayout { get; set; }

        /// <summary>
        /// The map styles to show in the control.
        /// </summary>
        public IEnumerable<MapStyle> MapStyles { get; set; }

        /// <summary>
        /// The style of the control.
        /// </summary>
        public ControlStyle Style { get; set; }
    }
}
