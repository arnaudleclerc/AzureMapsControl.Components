namespace AzureMapsControl.Components.Controls
{
    using System;

    public sealed class ControlNotAddedToMapException : Exception
    {
        internal ControlNotAddedToMapException() : base("This control has not been added to the map") { }
    }
}
