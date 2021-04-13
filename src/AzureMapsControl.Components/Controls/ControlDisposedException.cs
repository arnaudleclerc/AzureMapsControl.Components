namespace AzureMapsControl.Components.Controls
{
    using System;

    public sealed class ControlDisposedException : Exception
    {
        internal ControlDisposedException() : base("This control has already been disposed") { }
    }
}
