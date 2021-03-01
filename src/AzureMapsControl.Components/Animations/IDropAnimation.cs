namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;

    public interface IDropAnimation : IUpdatableAnimation<DropAnimationOptions>, ISeekAnimation, IPausableAnimation, IAnimation
    {
    }
}
