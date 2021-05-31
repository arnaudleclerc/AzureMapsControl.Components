namespace AzureMapsControl.Components.Indoor
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class IndoorManagerEventType : AtlasEventType
    {
        public static readonly IndoorManagerEventType FacilityChanged = new("facilitychanged");
        public static readonly IndoorManagerEventType LevelChanged = new("levelchanged");

        private IndoorManagerEventType(string atlasEvent) : base(atlasEvent) { }
    }
}
