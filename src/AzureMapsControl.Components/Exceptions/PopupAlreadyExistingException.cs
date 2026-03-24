
using System;

namespace AzureMapsControl.Components.Exceptions;
public sealed class PopupAlreadyExistingException : Exception
{
    internal PopupAlreadyExistingException(string id) : base($"A data source with the id {id} has already been added") { }
    internal PopupAlreadyExistingException(string id, Exception innerException) : base($"A data source with the id {id} has already been added", innerException) { }
}
