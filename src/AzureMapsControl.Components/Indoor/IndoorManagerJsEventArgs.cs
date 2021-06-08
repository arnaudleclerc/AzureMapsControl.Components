namespace AzureMapsControl.Components.Indoor
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class IndoorManagerJsEventArgs
    {
        public string Type { get; set; }
        public string FacilityId { get; set; }
        public int LevelNumber { get; set; }
        public string PrevFacilityId { get; set; }
        public int PrevLevelNumber { get; set; }
    }
}
