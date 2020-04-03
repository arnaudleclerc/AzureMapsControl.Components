namespace AzureMapsControl.Components.Markers
{
    using System.Collections.Generic;

    internal class HtmlMarkerCreationOptions
    {
        public IEnumerable<string> Events { get; set; }
        public HtmlMarkerOptions Options { get; set; }
    }
}
