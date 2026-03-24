
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Popups;
[ExcludeFromCodeCoverage]
public sealed class PopupEventArgs
{
    public string Id { get; set; }
    public string Type { get; set; }
}
