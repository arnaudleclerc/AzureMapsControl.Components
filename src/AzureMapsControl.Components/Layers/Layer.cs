namespace AzureMapsControl.Components.Layers
{
    public abstract class Layer
    {
        public string Id { get; }

        internal LayerType Type { get; private set; }

        internal Layer(string id, LayerType type)
        {
            Id = id;
            Type = type;
        }
    }
}
