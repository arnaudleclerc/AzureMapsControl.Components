namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Runtime;

    internal sealed class FlowingDashedLineAnimation : Animation<MovingDashLineOptions>, IFlowingDashedLineAnimation
    {
        public FlowingDashedLineAnimation(string id, IMapJsRuntime jsRuntime) : base(id, jsRuntime)
        {
        }

        public override Task SeekAsync(decimal progress) => throw new NotSupportedException($"{nameof(SeekAsync)} is not supported on {nameof(FlowingDashedLineAnimation)}");

        public override Task SetOptionsAsync(MovingDashLineOptions options) => throw new NotSupportedException($"{nameof(SetOptionsAsync)} is not supported on {nameof(FlowingDashedLineAnimation)}");
    }
}
