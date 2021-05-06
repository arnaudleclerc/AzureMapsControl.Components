namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options for the FullscreenControl.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class FullScreenControlOptions : IControlOptions
    {
        /// <summary>
        /// The HTML element that should be made fullscreen. If not specified, the map container element will be used. 
        /// If a string is passed in, it will first be used with `document.getElementById` and if null, will then use `document.querySelector`.
        /// </summary>
        public string Container { get; set; }

        /// <summary>
        /// Specifies if the control should be hidden if fullscreen is not supported by the browser. 
        /// </summary>
        public bool? HideIfUnsupported { get; set; }

        /// <summary>
        /// The style of the control. Can be; light, dark, auto, or any CSS3 color. When set to auto, the style will change based on the map style.
        /// Overridden if device is in high contrast mode.
        /// </summary>
        public Either<ControlStyle, string> Style { get; set; }
    }
}
