namespace AzureMapsControl.Components.Exceptions
{
    using System;

    public sealed class PopupAlreadyRemovedException : Exception
    {
        internal PopupAlreadyRemovedException() : base("This popup has already been removed") { }
    }
}
