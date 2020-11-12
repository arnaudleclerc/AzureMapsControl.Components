namespace AzureMapsControl.Components.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when trying to add a data source with an already existing ID
    /// </summary>
    public sealed class SourceAlreadyExistingException : Exception
    {
        internal SourceAlreadyExistingException(string id) : base($"A source with the id {id} has already been added") { }
    }
}
