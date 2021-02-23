namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    internal sealed class AnimationService : IAnimationService
    {
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger _logger;
        public AnimationService(IMapJsRuntime jsRuntime, ILogger<AnimationService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async Task<PlayableAnimation> MoveAlongPathAsync(LineString line, DataSource lineSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "LineId", line.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "lineSource", lineSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new PlayableAnimation(Guid.NewGuid().ToString(), options, _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, line.Id, lineSource.Id, pin.Id, pinSource.Id, options);
            return animation;
        }

        public async Task<PlayableAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Snakeline, "Calling Snakeline");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "LineId", line.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "SourceId", source.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "Options", options);
            var animation = new PlayableAnimation(Guid.NewGuid().ToString(), options, _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), animation.Id, line.Id, source.Id, options);
            return animation;
        }
    }
}
