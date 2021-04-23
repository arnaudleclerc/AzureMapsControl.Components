namespace AzureMapsControl.Components.Data
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class DataSourceEventType : AtlasEventType
    {
        public static readonly DataSourceEventType DataSourceUpdated = new DataSourceEventType("datasourceupdated");
        public static readonly DataSourceEventType DataAdded = new DataSourceEventType("dataadded");
        public static readonly DataSourceEventType DataRemoved = new DataSourceEventType("dataremoved");
        public static readonly DataSourceEventType SourceAdded = new DataSourceEventType("sourceadded");
        public static readonly DataSourceEventType SourceRemoved = new DataSourceEventType("sourceremoved");

        private DataSourceEventType(string atlasEvent) : base(atlasEvent)
        {
        }
    }
}
