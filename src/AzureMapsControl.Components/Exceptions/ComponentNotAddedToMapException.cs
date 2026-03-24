namespace AzureMapsControl.Components.Exceptions
{
    using System;

    public sealed class ComponentNotAddedToMapException : Exception
    {
        internal ComponentNotAddedToMapException() : base("This component has not been added to the map") { }
        internal ComponentNotAddedToMapException(string message) : base(message) { }
        internal ComponentNotAddedToMapException(string message, Exception innerException) : base(message, innerException) { }
    }
}
