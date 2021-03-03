namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;

    public interface ISetCoordinatesAnimation : IUpdatableAnimation<SetCoordinatesAnimationOptions>, IPausableAnimation, ISeekAnimation, IAnimation
    {
    }
}
