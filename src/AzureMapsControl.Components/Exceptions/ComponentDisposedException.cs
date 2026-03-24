namespace AzureMapsControl.Components.Exceptions
{
    using System;

    public class ComponentDisposedException : Exception
    {
        internal ComponentDisposedException() : base("This element has already been disposed") { }
        internal ComponentDisposedException(string message) : base(message) { }
        internal ComponentDisposedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
