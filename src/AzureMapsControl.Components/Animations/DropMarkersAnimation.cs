
using AzureMapsControl.Components.Animations.Options;
using AzureMapsControl.Components.Runtime;

namespace AzureMapsControl.Components.Animations;
internal sealed class DropMarkersAnimation : Animation<DropMarkersAnimationOptions>, IDropMarkersAnimation
{
    public DropMarkersAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
    {
    }
}
