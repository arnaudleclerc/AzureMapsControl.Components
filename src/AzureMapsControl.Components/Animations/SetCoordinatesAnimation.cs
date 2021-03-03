namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    internal sealed class SetCoordinatesAnimation : Animation<SetCoordinatesAnimationOptions>, ISetCoordinatesAnimation
    {
        public SetCoordinatesAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }
    }
}
