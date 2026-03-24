
using AzureMapsControl.Components.Exceptions;

namespace AzureMapsControl.Components.Controls;
public sealed class ControlDisposedException : ComponentDisposedException
{
    internal ControlDisposedException() : base("This control has already been disposed") { }
}
