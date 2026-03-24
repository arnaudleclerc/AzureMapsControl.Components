
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Indoor;
[ExcludeFromCodeCoverage]
internal record IndoorManagerJsEventArgs
{
    public string Type { get; init; }
    public string FacilityId { get; init; }
    public int LevelNumber { get; init; }
    public string PrevFacilityId { get; init; }
    public int PrevLevelNumber { get; init; }
}
