namespace AzureMapsControl.Components.Indoor
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class IndoorManagerEventArgs
    {
        /// <summary>
        /// The facility that the map is changing to.
        /// </summary>
        public string FacilityId { get; set; }
        
        /// <summary>
        /// The level ordinal that the map is changing to.
        /// </summary>
        public int LevelNumber { get; set; }
        
        /// <summary>
        /// The facility that the map is changing out of.
        /// </summary>
        public string PrevFacilityId { get; set; }

        /// <summary>
        /// The level number that the map is changing out of.
        /// </summary>
        public int PrevLevelNumber { get; set; }
    }
}
