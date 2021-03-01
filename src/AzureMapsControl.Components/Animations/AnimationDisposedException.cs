namespace AzureMapsControl.Components.Animations
{
    using System;

    public sealed class AnimationDisposedException : Exception
    {
        internal AnimationDisposedException(): base("This animation has already been disposed") { }
    }
}
