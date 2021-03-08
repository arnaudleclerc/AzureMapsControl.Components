namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;
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

        public async Task<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "pathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, pinSource.Id, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, null, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, pinSource.Id, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, null, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<ISnakeLineAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Snakeline, "Calling Snakeline");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "LineId", line.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "SourceId", source.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "Options", options);

            var animation = new SnakeLineAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), animation.Id, line.Id, source.Id, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IFlowingDashedLineAnimation> FlowingDashedLineAsync(LineLayer layer, MovingDashLineOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Calling Snakeline");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "LayerId", layer.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Options", options);

            var animation = new FlowingDashedLineAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.FlowingDashedLine.ToAnimationNamespace(), animation.Id, layer.Id, options);
            return animation;
        }

        public async Task<IDropMarkersAnimation> DropMarkersAsync(IEnumerable<HtmlMarker> markers, decimal? height = null, DropMarkersAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_DropMarkers, "Calling DropMarkersAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Markers", markers);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Height", height);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Options", options);
            _mapService.Map.HtmlMarkers = (_mapService.Map.HtmlMarkers ?? Array.Empty<HtmlMarker>()).Concat(markers);

            var parameters = _mapService.Map.GetHtmlMarkersCreationParameters(markers);
            var animation = new DropMarkersAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), animation.Id, parameters.MarkerOptions, height, options, parameters.InvokeHelper);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IDropMarkersAnimation> DropMarkerAsync(HtmlMarker marker, decimal? height = null, DropMarkersAnimationOptions options = default) => await DropMarkersAsync(new[] { marker }, height, options);

        public async Task<IGroupAnimation> GroupAnimationAsync(IEnumerable<IAnimation> animations, GroupAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_GroupAnimations, "Calling GroupAnimationAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_GroupAnimations, "Animations", animations);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_GroupAnimations, "Options", options);

            var animation = new GroupAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.GroupAnimations.ToAnimationNamespace(), animation.Id, animations.Select(a => a.Id), options);
            return animation;
        }

        public async Task<IDropAnimation> DropAsync(IEnumerable<Point> points, DataSource source, decimal? height = null, DropAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Drop, "Calling DropAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Points", points);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Source", source);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Height", height);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Options", options);

            var animation = new DropAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Drop.ToAnimationNamespace(), animation.Id, points, source.Id, height, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IDropAnimation> DropAsync(Point point, DataSource source, decimal? height = null, DropAnimationOptions options = default)
            => await DropAsync(new[] { point }, source, height, options);

        public async Task<ISetCoordinatesAnimation> SetCoordinatesAsync<TPosition>(Geometry<TPosition> geometry, DataSource source, TPosition newCoordinates, SetCoordinatesAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_SetCoordinates, "Calling SetCoordinatesAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Geometry", geometry);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Source", source);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "New Coordinates", newCoordinates);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new SetCoordinatesAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), animation.Id, geometry.Id, source.Id, newCoordinates, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<ISetCoordinatesAnimation> SetCoordinatesAsync(HtmlMarker marker, Position newCoordinates, SetCoordinatesAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_SetCoordinates, "Calling SetCoordinatesAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Marker", marker);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "New Coordinates", newCoordinates);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new SetCoordinatesAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), animation.Id, marker.Id, null, newCoordinates, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IMorphAnimation> MorphAsync<T>(Geometry geometry, DataSource source, T newGeometry, MorphAnimationOptions options = default)
            where T : Geometry
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MorphAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Geometry", geometry);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Source", source);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "New Geometry", newGeometry);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new MorphAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Morph.ToAnimationNamespace(), animation.Id, geometry.Id, source.Id, newGeometry, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async Task<IMoveAlongRouteAnimation> MoveAlongRouteAsync(IEnumerable<RoutePoint> points, Point pin, DataSource pinSource, RoutePathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MoveAlongRoute");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Points", points);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Pin", pin);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Source", pinSource);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new MoveAlongRouteAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), animation.Id, points, pinSource.Id, pin.Id, options);
            return animation;
        }

        public async Task<IMoveAlongRouteAnimation> MoveAlongRouteAsync(IEnumerable<RoutePoint> points, HtmlMarker pin, RoutePathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MoveAlongRoute");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Points", points);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Pin", pin);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new MoveAlongRouteAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), animation.Id, points, null, pin.Id, options);
            return animation;
        }
    }
}
