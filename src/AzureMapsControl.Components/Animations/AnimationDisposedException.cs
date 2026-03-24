
using AzureMapsControl.Components.Exceptions;

namespace AzureMapsControl.Components.Animations;
public sealed class AnimationDisposedException : ComponentDisposedException
{
    internal AnimationDisposedException(): base("This animation has already been disposed") { }
}
