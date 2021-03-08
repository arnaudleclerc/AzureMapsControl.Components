namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Runtime;

    internal sealed class MoveAlongRouteAnimation : Animation<RoutePathAnimationOptions>, IMoveAlongRouteAnimation
    {
        public MoveAlongRouteAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }

        public override Task PauseAsync() => throw new NotSupportedException($"{nameof(PauseAsync)} is not supported by {nameof(MoveAlongRouteAnimation)}");
        public override Task PlayAsync() => throw new NotSupportedException($"{nameof(PlayAsync)} is not supported by {nameof(MoveAlongRouteAnimation)}");
        public override Task ResetAsync() => throw new NotSupportedException($"{nameof(ResetAsync)} is not supported by {nameof(MoveAlongRouteAnimation)}");
        public override Task SeekAsync(decimal progress) => throw new NotSupportedException($"{nameof(SeekAsync)} is not supported by {nameof(MoveAlongRouteAnimation)}");
        public override Task SetOptionsAsync(RoutePathAnimationOptions options) => throw new NotSupportedException($"{nameof(SetOptionsAsync)} is not supported by {nameof(MoveAlongRouteAnimation)}");
        public override Task StopAsync() => throw new NotSupportedException($"{nameof(StopAsync)} is not supported by {nameof(MoveAlongRouteAnimation)}");
    }
}
