
using System;

namespace AzureMapsControl.Components.Exceptions;
public sealed class ComponentNotAddedToMapException : Exception
{
    internal ComponentNotAddedToMapException() : base("This component has not been added to the map") { }
    internal ComponentNotAddedToMapException(string message) : base(message) { }
    internal ComponentNotAddedToMapException(string message, Exception innerException) : base(message, innerException) { }
}
