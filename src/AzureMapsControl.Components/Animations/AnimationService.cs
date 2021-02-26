namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    internal sealed class AnimationService : IAnimationService
    {
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger _logger;
        private readonly IMapService _mapService;

        public AnimationService(IMapJsRuntime jsRuntime, ILogger<AnimationService> logger, IMapService mapService)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
            _mapService = mapService;
        }

        public async Task<IUpdatableAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "pathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new UpdatableAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, pinSource.Id, options);
            return animation;
        }

        public async Task<IUpdatableAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new UpdatableAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, null, options);
            return animation;
        }

        public async Task<IUpdatableAnimation> MoveAlongPathAsync(IEnumerable<Position> path, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new UpdatableAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, pinSource.Id, options);
            return animation;
        }

        public async Task<IUpdatableAnimation> MoveAlongPathAsync(IEnumerable<Position> path, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new UpdatableAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, null, options);
            return animation;
        }

        public async Task<IUpdatableAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Snakeline, "Calling Snakeline");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "LineId", line.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "SourceId", source.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "Options", options);
            var animation = new UpdatableAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), animation.Id, line.Id, source.Id, options);
            return animation;
        }

        public async Task<IAnimation> FlowingDashedLineAsync(LineLayer layer, MovingDashLineOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Calling Snakeline");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "LayerId", layer.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Options", options);
            var animation = new Animation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.FlowingDashedLine.ToAnimationNamespace(), animation.Id, layer.Id, options);
            return animation;
        }

        public async Task<IUpdatableAnimation> DropMarkersAsync(IEnumerable<HtmlMarker> markers, decimal? height = null, AnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_DropMarkers, "Calling DropMarkersAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Markers", markers);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Height", height);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Options", options);
            _mapService.Map.HtmlMarkers = (_mapService.Map.HtmlMarkers ?? Array.Empty<HtmlMarker>()).Concat(markers);
            var parameters = _mapService.Map.GetHtmlMarkersCreationParameters(markers);
            var animation = new UpdatableAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), animation.Id, parameters.MarkerOptions, height, options, parameters.InvokeHelper);
            return animation;
        }

        public async Task<IUpdatableAnimation> DropMarkerAsync(HtmlMarker marker, decimal? height = null, AnimationOptions options = default) => await DropMarkersAsync(new [] { marker }, height, options);
    }
}
