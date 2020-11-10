namespace AzureMapsControl.Components.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when trying to add a data source with an already existing ID
    /// </summary>
    public sealed class DataSourceAlreadyExistingException : Exception
    {
        internal DataSourceAlreadyExistingException(string id) : base($"A data source with the id {id} has already been added") { }
    }
}
