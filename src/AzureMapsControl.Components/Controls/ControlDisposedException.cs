namespace AzureMapsControl.Components.Controls
{
    using AzureMapsControl.Components.Exceptions;

    public sealed class ControlDisposedException : ComponentDisposedException
    {
        internal ControlDisposedException() : base("This control has already been disposed") { }
    }
}
