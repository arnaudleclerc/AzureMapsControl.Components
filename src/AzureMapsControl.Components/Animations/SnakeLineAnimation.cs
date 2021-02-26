namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    internal sealed class SnakeLineAnimation : Animation<SnakeLineAnimationOptions>, ISnakeLineAnimation
    {
        internal SnakeLineAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }
    }
}
