namespace AzureMapsControl.Components.Exceptions
{
    using System;

    public sealed class ComponentNotAddedToMapException : Exception
    {
        internal ComponentNotAddedToMapException() : base("This component has not been added to the map") { }
    }
}
