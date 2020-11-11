namespace AzureMapsControl.Components.Exceptions
{
    using System;

    public sealed class PopupAlreadyExistingException : Exception
    {
        internal PopupAlreadyExistingException(string id) : base($"A data source with the id {id} has already been added") { }
    }
}
