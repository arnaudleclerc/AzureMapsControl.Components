
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Map;

namespace AzureMapsControl.Components.Controls;
/// <summary>
/// The options for a StyleControl object.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class StyleControlOptions : IControlOptions
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
