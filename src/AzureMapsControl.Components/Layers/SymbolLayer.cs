namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// Renders point based data as symbols on the map using text and/or icons.
    /// Symbols can also be created for line and polygon data as well.
    /// </summary>
    public sealed class SymbolLayer : Layer<SymbolLayerOptions>
    {
        public SymbolLayer(): this(Guid.NewGuid().ToString()) { }
        public SymbolLayer(string id) : base(id, LayerType.SymbolLayer) { }
    }
}
