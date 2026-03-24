
using AzureMapsControl.Components.Animations.Options;
using AzureMapsControl.Components.Runtime;

namespace AzureMapsControl.Components.Animations;
internal sealed class MoveAlongPathAnimation : Animation<MoveAlongPathAnimationOptions>, IMoveAlongPathAnimation
{
    public MoveAlongPathAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
    {
    }
}
