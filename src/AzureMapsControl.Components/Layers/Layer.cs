namespace AzureMapsControl.Components.Layers
{
    using System;

    using AzureMapsControl.Components.Events;

    public abstract class Layer
    {
        public string Id { get; }
        internal LayerType Type { get; private set; }

        internal Layer(string id, LayerType type)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            Type = type;
        }

        internal abstract LayerOptions GetLayerOptions();

    }

    public abstract class Layer<T> : Layer
        where T : AtlasEventType
    {
        public EventActivationFlags<T> EventActivationFlags { get; }
        internal Layer(string id, LayerType type) : base(id, type)
        {
        }

    }

    public abstract class Layer<T, U> : Layer<U>
        where T : LayerOptions
        where U : AtlasEventType
    {
        public T Options { get; set; }

        internal Layer(string id, LayerType type) : base(id, type)
        {
        }

        internal override LayerOptions GetLayerOptions() => Options;
    }
}
