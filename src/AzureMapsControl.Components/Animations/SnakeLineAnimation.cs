
using AzureMapsControl.Components.Animations.Options;
using AzureMapsControl.Components.Runtime;

namespace AzureMapsControl.Components.Animations;
internal sealed class SnakeLineAnimation : Animation<SnakeLineAnimationOptions>, ISnakeLineAnimation
{
    internal SnakeLineAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
    {
    }
}
