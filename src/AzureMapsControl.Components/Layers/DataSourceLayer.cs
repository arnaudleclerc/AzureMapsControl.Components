namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class DataSourceLayer<T> : Layer<T>
        where T : LayerOptions
    {
        public string DataSourceId { get; }

        internal DataSourceLayer(string id, LayerType type, string dataSourceId) : base(id, type) => DataSourceId = dataSourceId;
    }
}
