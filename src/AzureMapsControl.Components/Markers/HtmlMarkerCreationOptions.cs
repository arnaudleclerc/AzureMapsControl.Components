namespace AzureMapsControl.Components.Markers
{
    using System.Collections.Generic;

    internal class HtmlMarkerCreationOptions : HtmlMarkerUpdateOptions
    {
        public IEnumerable<string> Events { get; set; }
    }
}
