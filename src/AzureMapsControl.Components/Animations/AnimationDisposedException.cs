namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Exceptions;

    public sealed class AnimationDisposedException : ComponentDisposedException
    {
        internal AnimationDisposedException(): base("This animation has already been disposed") { }
    }
}
