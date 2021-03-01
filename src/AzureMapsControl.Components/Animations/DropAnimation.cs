namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    internal sealed class DropAnimation : Animation<DropAnimationOptions>, IDropAnimation
    {
        public DropAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }
    }
}
