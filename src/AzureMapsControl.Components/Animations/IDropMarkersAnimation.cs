namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;

    public interface IDropMarkersAnimation : IUpdatableAnimation<DropMarkersAnimationOptions>, ISeekAnimation, IPausableAnimation, IAnimation
    {
    }
}
