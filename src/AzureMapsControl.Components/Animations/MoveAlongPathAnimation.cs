namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Runtime;

    internal sealed class MoveAlongPathAnimation : Animation<MoveAlongPathAnimationOptions>, IMoveAlongPathAnimation
    {
        public MoveAlongPathAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }
    }
}
