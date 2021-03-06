﻿namespace AzureMapsControl.Components.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Guards;
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

        public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

            Require.NotNull(path, nameof(path));
            Require.NotNull(pathSource, nameof(pathSource));
            Require.NotNull(pin, nameof(pin));
            Require.NotNull(pinSource, nameof(pinSource));

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

        public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

            Require.NotNull(path, nameof(path));
            Require.NotNull(pathSource, nameof(pathSource));
            Require.NotNull(pin, nameof(pin));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathSource", pathSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, null, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

            Require.NotNull(path, nameof(path));
            Require.NotNull(pin, nameof(pin));
            Require.NotNull(pinSource, nameof(pinSource));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, pinSource.Id, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

            Require.NotNull(path, nameof(path));
            Require.NotNull(pin, nameof(pin));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

            var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, null, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<ISnakeLineAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Snakeline, "Calling Snakeline");

            Require.NotNull(line, nameof(line));
            Require.NotNull(source, nameof(source));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "LineId", line.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "SourceId", source.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "Options", options);

            var animation = new SnakeLineAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), animation.Id, line.Id, source.Id, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<IFlowingDashedLineAnimation> FlowingDashedLineAsync(LineLayer layer, MovingDashLineOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Calling Snakeline");

            Require.NotNull(layer, nameof(layer));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "LayerId", layer.Id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Options", options);

            var animation = new FlowingDashedLineAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.FlowingDashedLine.ToAnimationNamespace(), animation.Id, layer.Id, options);
            return animation;
        }

        public async ValueTask<IDropMarkersAnimation> DropMarkersAsync(IEnumerable<HtmlMarker> markers, decimal? height = null, DropMarkersAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_DropMarkers, "Calling DropMarkersAsync");

            Require.NotNull(markers, nameof(markers));

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

        public async ValueTask<IDropMarkersAnimation> DropMarkerAsync(HtmlMarker marker, decimal? height = null, DropMarkersAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_DropMarkers, "Calling DropMarkersAsync");

            Require.NotNull(marker, nameof(marker));
            return await DropMarkersAsync(new[] { marker }, height, options);
        }

        public async ValueTask<IGroupAnimation> GroupAnimationAsync(IEnumerable<IAnimation> animations, GroupAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_GroupAnimations, "Calling GroupAnimationAsync");

            Require.NotNull(animations, nameof(animations));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_GroupAnimations, "Animations", animations);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_GroupAnimations, "Options", options);

            var animation = new GroupAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.GroupAnimations.ToAnimationNamespace(), animation.Id, animations.Select(a => a.Id), options);
            return animation;
        }

        public async ValueTask<IDropAnimation> DropAsync(IEnumerable<Point> points, DataSource source, decimal? height = null, DropAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Drop, "Calling DropAsync");

            Require.NotNull(points, nameof(points));
            Require.NotNull(source, nameof(source));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Points", points);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Source", source);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Height", height);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Options", options);

            var animation = new DropAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Drop.ToAnimationNamespace(), animation.Id, points, source.Id, height, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<IDropAnimation> DropAsync(Point point, DataSource source, decimal? height = null, DropAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Drop, "Calling DropAsync");

            Require.NotNull(point, nameof(point));
            return await DropAsync(new[] { point }, source, height, options);
        }

        public async ValueTask<ISetCoordinatesAnimation> SetCoordinatesAsync<TPosition>(Geometry<TPosition> geometry, DataSource source, TPosition newCoordinates, SetCoordinatesAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_SetCoordinates, "Calling SetCoordinatesAsync");

            Require.NotNull(geometry, nameof(geometry));
            Require.NotNull(source, nameof(source));
            Require.NotNull(newCoordinates, nameof(newCoordinates));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Geometry", geometry);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Source", source);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "New Coordinates", newCoordinates);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new SetCoordinatesAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), animation.Id, geometry.Id, source.Id, newCoordinates, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<ISetCoordinatesAnimation> SetCoordinatesAsync(HtmlMarker marker, Position newCoordinates, SetCoordinatesAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_SetCoordinates, "Calling SetCoordinatesAsync");

            Require.NotNull(marker, nameof(marker));
            Require.NotNull(newCoordinates, nameof(newCoordinates));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Marker", marker);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "New Coordinates", newCoordinates);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new SetCoordinatesAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), animation.Id, marker.Id, null, newCoordinates, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<IMorphAnimation> MorphAsync<T>(Geometry geometry, DataSource source, T newGeometry, MorphAnimationOptions options = default)
            where T : Geometry
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MorphAsync");

            Require.NotNull(geometry, nameof(geometry));
            Require.NotNull(source, nameof(source));
            Require.NotNull(newGeometry, nameof(newGeometry));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Geometry", geometry);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Source", source);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "New Geometry", newGeometry);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new MorphAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Morph.ToAnimationNamespace(), animation.Id, geometry.Id, source.Id, newGeometry, options);
            animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
            return animation;
        }

        public async ValueTask<IMoveAlongRouteAnimation> MoveAlongRouteAsync(IEnumerable<RoutePoint> points, Point pin, DataSource pinSource, RoutePathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MoveAlongRoute");

            Require.NotNull(points, nameof(points));
            Require.NotNull(pin, nameof(pin));
            Require.NotNull(pinSource, nameof(pinSource));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Points", points);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Pin", pin);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Source", pinSource);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new MoveAlongRouteAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), animation.Id, points, pinSource.Id, pin.Id, options);
            return animation;
        }

        public async ValueTask<IMoveAlongRouteAnimation> MoveAlongRouteAsync(IEnumerable<RoutePoint> points, HtmlMarker pin, RoutePathAnimationOptions options = default)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MoveAlongRoute");

            Require.NotNull(points, nameof(points));
            Require.NotNull(pin, nameof(pin));

            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Points", points);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Pin", pin);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

            var animation = new MoveAlongRouteAnimation(Guid.NewGuid().ToString(), _jsRuntime);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), animation.Id, points, null, pin.Id, options);
            return animation;
        }
    }
}
