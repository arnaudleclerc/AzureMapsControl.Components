
using AzureMapsControl.Components.Animations.Options;
using AzureMapsControl.Components.Runtime;

namespace AzureMapsControl.Components.Animations;
internal sealed class SetCoordinatesAnimation : Animation<SetCoordinatesAnimationOptions>, ISetCoordinatesAnimation
{
    public SetCoordinatesAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
    {
    }
}
