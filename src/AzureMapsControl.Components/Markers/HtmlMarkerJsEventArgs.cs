namespace AzureMapsControl.Components.Markers
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerJsEventArgs
    {
        public string MarkerId { get; set; }
        public string Type { get; set; }
        public HtmlMarkerOptions Options { get; set; }
    }
}
