namespace AzureMapsControl.Components.Atlas.FormatOptions
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class HyperLinkFormatOptions
    {
        /// <summary>
        /// Specifies the text that should be displayed to the user.
        /// If not specified, the hyperlink will be displayed.
        /// If the hyperlink is an image, this will be set as the "alt" property of the img tag.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Specifies if the hyperlink is for an image.
        /// If set to true, the hyperlink will be loaded into an img tag and when clicked, will open the hyperlink to the image.
        /// </summary>
        public bool? IsImage { get; set; }

        /// <summary>
        /// Specifies a scheme to prepend to a hyperlink such as 'mailto:' or 'tel:'.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Format options for hyperlink strings.
        /// </summary>
        public HyperLinkFormatOptionsTarget Target { get; set; }
    }
}
