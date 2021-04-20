namespace AzureMapsControl.Components.Data
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class DataSourceEventType : AtlasEventType
    {
        public static DataSourceEventType DataSourceUpdated = new DataSourceEventType("datasourceupdated");
        public static DataSourceEventType DataAdded = new DataSourceEventType("dataadded");
        public static DataSourceEventType DataRemoved = new DataSourceEventType("dataremoved");
        public static DataSourceEventType SourceAdded = new DataSourceEventType("sourceadded");
        public static DataSourceEventType SourceRemoved = new DataSourceEventType("sourceremoved");

        private DataSourceEventType(string atlasEvent) : base(atlasEvent)
        {
        }
    }
}
