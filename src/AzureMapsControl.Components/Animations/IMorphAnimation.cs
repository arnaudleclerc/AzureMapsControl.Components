namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;

    public interface IMorphAnimation : IUpdatableAnimation<MorphAnimationOptions>, ISeekAnimation, IPausableAnimation, IAnimation
    {
    }
}
