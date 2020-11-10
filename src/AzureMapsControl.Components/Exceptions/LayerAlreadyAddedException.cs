namespace AzureMapsControl.Components.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a layer with a specific ID has already been added
    /// </summary>
    public sealed class LayerAlreadyAddedException : Exception
    {
        internal LayerAlreadyAddedException(string layerId): base($"A layer with the same id {layerId} has already been added") { }
    }
}
