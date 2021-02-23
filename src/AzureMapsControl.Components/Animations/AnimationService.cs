namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Markers;
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

        public async Task<PlayableAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "pathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new PlayableAnimation(Guid.NewGuid().ToString(), options, _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, pinSource.Id, options);
            return animation;
        }

        public async Task<PlayableAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, HtmlMarker pin, MoveAlongPathAnimationOptions options = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new PlayableAnimation(Guid.NewGuid().ToString(), options, _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, null, options);
            return animation;
        }

        public async Task<PlayableAnimation> MoveAlongPathAsync(IEnumerable<Position> path, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new PlayableAnimation(Guid.NewGuid().ToString(), options, _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, pinSource.Id, options);
            return animation;
        }

        public async Task<PlayableAnimation> MoveAlongPathAsync(IEnumerable<Position> path, HtmlMarker pin, MoveAlongPathAnimationOptions options = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new PlayableAnimation(Guid.NewGuid().ToString(), options, _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, null, options);
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
