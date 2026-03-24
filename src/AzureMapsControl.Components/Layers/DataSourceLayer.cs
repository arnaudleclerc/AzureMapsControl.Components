
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Layers;
[ExcludeFromCodeCoverage]
public abstract class DataSourceLayer<T> : Layer<T>
    where T : LayerOptions, new()
{
    public string DataSourceId { get; }

    internal DataSourceLayer(string id, LayerType type, string dataSourceId) : base(id, type) => DataSourceId = dataSourceId;
}
