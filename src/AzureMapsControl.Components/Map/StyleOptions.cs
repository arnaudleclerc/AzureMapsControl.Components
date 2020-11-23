namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class StyleOptions
    {
        /// <summary>
        /// If true the map will automatically resize whenever the window's size changes. Otherwise map.resize() must be called.
        /// Default true.
        /// </summary>
        public bool AutoResize { get; set; } = true;

        /// <summary>
        /// The language of the map labels.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Sets the lighting options of the map.
        /// </summary>
        public LightOptions Light { get; set; }

        /// <summary>
        /// If true, the map's canvas can be exported to a PNG using map.getCanvas().toDataURL().
        /// </summary>
        public bool PreserveDrawingBuffer { get; set; }

        /// <summary>
        /// Specifies if multiple copies of the world should be rendered when zoomed out. Default true
        /// </summary>
        public bool RenderWorldCopies { get; set; } = true;

        /// <summary>
        /// Specifies if buildings will be rendered with their models. If false all buildings will be rendered as just their footprints
        /// </summary>
        public bool ShowBuildingModels { get; set; }

        /// <summary>
        /// Specifies if the feedback link should be displayed on the map or not. Default true
        /// </summary>
        public bool ShowFeedbackLink { get; set; } = true;

        /// <summary>
        /// Specifies if the Microsoft logo should be hidden or not. If set to true a Microsoft copyright string will be added to the map. Default true
        /// </summary>
        public bool ShowLogo { get; set; } = true;

        /// <summary>
        /// Specifies if the map should render an outline around each tile and the tile ID. These tile boundaries are useful for debugging. The uncompressed file size of the first vector source is drawn in the top left corner of each tile, next to the tile ID
        /// </summary>
        public bool ShowTileBoundaries { get; set; }

        /// <summary>
        /// The name of the style to use when rendering the map
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Specifies which set of geopolitically disputed borders and labels are displayed on the map. The View parameter (also referred to as “user region parameter”) is a 2-letter ISO-3166 Country Code that will show the correct maps for that country/region. Country/Regions that are not on the View list or if unspecified will default to the “Unified” View
        /// </summary>
        public string View { get; set; }
    }
}
