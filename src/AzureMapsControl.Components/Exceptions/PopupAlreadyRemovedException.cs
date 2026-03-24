
using System;

namespace AzureMapsControl.Components.Exceptions;
public sealed class PopupAlreadyRemovedException : Exception
{
    internal PopupAlreadyRemovedException() : base("This popup has already been removed") { }
    internal PopupAlreadyRemovedException(string message) : base(message) { }
    internal PopupAlreadyRemovedException(string message, Exception innerException) : base(message, innerException) { }
}
