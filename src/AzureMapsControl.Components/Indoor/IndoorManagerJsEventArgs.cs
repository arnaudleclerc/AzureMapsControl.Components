
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Indoor;
[ExcludeFromCodeCoverage]
internal class IndoorManagerJsEventArgs
{
    public string Type { get; set; }
    public string FacilityId { get; set; }
    public int LevelNumber { get; set; }
    public string PrevFacilityId { get; set; }
    public int PrevLevelNumber { get; set; }
}
