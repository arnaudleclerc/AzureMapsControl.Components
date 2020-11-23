namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class StyleOptions
    {
        public bool AutoResize { get; set; }
        public string Language { get; set; }
        public LightOptions Light { get; set; }
        public bool PreserveDrawingBuffer { get; set; }
        public bool RenderWorldCopies { get; set; }
        public bool ShowBuildingModels { get; set; }
        public bool ShowFeedbackLink { get; set; }
        public bool ShowLogo { get; set; }
        public bool ShowTileBoundaries { get; set; }
        public string Style { get; set; }
        public string View { get; set; }
    }
}
