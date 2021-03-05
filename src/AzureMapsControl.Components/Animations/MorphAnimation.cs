namespace AzureMapsControl.Components.Animations
{
    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    internal sealed class MorphAnimation : Animation<MorphAnimationOptions>, IMorphAnimation
    {
        public MorphAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }
    }
}
