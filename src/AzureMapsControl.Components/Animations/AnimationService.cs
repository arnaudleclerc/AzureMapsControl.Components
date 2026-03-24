
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

namespace AzureMapsControl.Components.Animations;
internal sealed class AnimationService(IMapJsRuntime jsRuntime, ILogger<AnimationService> logger, IMapService mapService) : IAnimationService
{

    public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

        Require.NotNull(path, nameof(path));
        Require.NotNull(pathSource, nameof(pathSource));
        Require.NotNull(pin, nameof(pin));
        Require.NotNull(pinSource, nameof(pinSource));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "pathSource", pathSource.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

        var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, pinSource.Id, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

        Require.NotNull(path, nameof(path));
        Require.NotNull(pathSource, nameof(pathSource));
        Require.NotNull(pin, nameof(pin));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathId", path.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PathSource", pathSource.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

        var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path.Id, pathSource.Id, pin.Id, null, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

        Require.NotNull(path, nameof(path));
        Require.NotNull(pin, nameof(pin));
        Require.NotNull(pinSource, nameof(pinSource));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinSourceId", pinSource.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

        var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, pinSource.Id, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, HtmlMarker pin, MoveAlongPathAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_MoveAlongPath, "Calling MoveAlongPath");

        Require.NotNull(path, nameof(path));
        Require.NotNull(pin, nameof(pin));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Path", path);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "PinId", pin.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_MoveAlongPath, "Options", options);

        var animation = new MoveAlongPathAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), animation.Id, path, null, pin.Id, null, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<ISnakeLineAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Snakeline, "Calling Snakeline");

        Require.NotNull(line, nameof(line));
        Require.NotNull(source, nameof(source));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "LineId", line.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "SourceId", source.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Snakeline, "Options", options);

        var animation = new SnakeLineAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), animation.Id, line.Id, source.Id, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IFlowingDashedLineAnimation> FlowingDashedLineAsync(LineLayer layer, MovingDashLineOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Calling Snakeline");

        Require.NotNull(layer, nameof(layer));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "LayerId", layer.Id);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_FlowingDashedLine, "Options", options);

        var animation = new FlowingDashedLineAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.FlowingDashedLine.ToAnimationNamespace(), animation.Id, layer.Id, options).ConfigureAwait(false);
        return animation;
    }

    public async ValueTask<IDropMarkersAnimation> DropMarkersAsync(IEnumerable<HtmlMarker> markers, decimal? height = null, DropMarkersAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_DropMarkers, "Calling DropMarkersAsync");

        Require.NotNull(markers, nameof(markers));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Markers", markers);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Height", height);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_DropMarkers, "Options", options);
        mapService.Map.HtmlMarkers = (mapService.Map.HtmlMarkers ?? Array.Empty<HtmlMarker>()).Concat(markers);

        var parameters = mapService.Map.GetHtmlMarkersCreationParameters(markers);
        var animation = new DropMarkersAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), animation.Id, parameters.MarkerOptions, height, options, parameters.InvokeHelper).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IDropMarkersAnimation> DropMarkerAsync(HtmlMarker marker, decimal? height = null, DropMarkersAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_DropMarkers, "Calling DropMarkersAsync");

        Require.NotNull(marker, nameof(marker));
        return await DropMarkersAsync(new[] { marker }, height, options).ConfigureAwait(false);
    }

    public async ValueTask<IGroupAnimation> GroupAnimationAsync(IEnumerable<IAnimation> animations, GroupAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_GroupAnimations, "Calling GroupAnimationAsync");

        Require.NotNull(animations, nameof(animations));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_GroupAnimations, "Animations", animations);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_GroupAnimations, "Options", options);

        var animation = new GroupAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.GroupAnimations.ToAnimationNamespace(), animation.Id, animations.Select(a => a.Id), options).ConfigureAwait(false);
        return animation;
    }

    public async ValueTask<IDropAnimation> DropAsync(IEnumerable<Point> points, DataSource source, decimal? height = null, DropAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Drop, "Calling DropAsync");

        Require.NotNull(points, nameof(points));
        Require.NotNull(source, nameof(source));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Points", points);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Source", source);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Height", height);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Drop, "Options", options);

        var animation = new DropAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Drop.ToAnimationNamespace(), animation.Id, points, source.Id, height, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IDropAnimation> DropAsync(Point point, DataSource source, decimal? height = null, DropAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Drop, "Calling DropAsync");

        Require.NotNull(point, nameof(point));
        return await DropAsync(new[] { point }, source, height, options).ConfigureAwait(false);
    }

    public async ValueTask<ISetCoordinatesAnimation> SetCoordinatesAsync<TPosition>(Geometry<TPosition> geometry, DataSource source, TPosition newCoordinates, SetCoordinatesAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_SetCoordinates, "Calling SetCoordinatesAsync");

        Require.NotNull(geometry, nameof(geometry));
        Require.NotNull(source, nameof(source));
        Require.NotNull(newCoordinates, nameof(newCoordinates));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Geometry", geometry);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Source", source);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "New Coordinates", newCoordinates);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

        var animation = new SetCoordinatesAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), animation.Id, geometry.Id, source.Id, newCoordinates, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<ISetCoordinatesAnimation> SetCoordinatesAsync(HtmlMarker marker, Position newCoordinates, SetCoordinatesAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_SetCoordinates, "Calling SetCoordinatesAsync");

        Require.NotNull(marker, nameof(marker));
        Require.NotNull(newCoordinates, nameof(newCoordinates));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Marker", marker);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "New Coordinates", newCoordinates);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

        var animation = new SetCoordinatesAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), animation.Id, marker.Id, null, newCoordinates, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IMorphAnimation> MorphAsync<T>(Geometry geometry, DataSource source, T newGeometry, MorphAnimationOptions options = default)
        where T : Geometry
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MorphAsync");

        Require.NotNull(geometry, nameof(geometry));
        Require.NotNull(source, nameof(source));
        Require.NotNull(newGeometry, nameof(newGeometry));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Geometry", geometry);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Source", source);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "New Geometry", newGeometry);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

        var animation = new MorphAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Morph.ToAnimationNamespace(), animation.Id, geometry.Id, source.Id, newGeometry, options).ConfigureAwait(false);
        animation.Disposed = options.DisposeOnComplete.GetValueOrDefault();
        return animation;
    }

    public async ValueTask<IMoveAlongRouteAnimation> MoveAlongRouteAsync(IEnumerable<RoutePoint> points, Point pin, DataSource pinSource, RoutePathAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MoveAlongRoute");

        Require.NotNull(points, nameof(points));
        Require.NotNull(pin, nameof(pin));
        Require.NotNull(pinSource, nameof(pinSource));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Points", points);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Pin", pin);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Source", pinSource);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

        var animation = new MoveAlongRouteAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), animation.Id, points, pinSource.Id, pin.Id, options).ConfigureAwait(false);
        return animation;
    }

    public async ValueTask<IMoveAlongRouteAnimation> MoveAlongRouteAsync(IEnumerable<RoutePoint> points, HtmlMarker pin, RoutePathAnimationOptions options = default)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AnimationService_Morph, "Calling MoveAlongRoute");

        Require.NotNull(points, nameof(points));
        Require.NotNull(pin, nameof(pin));

        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Points", points);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_Morph, "Pin", pin);
        logger?.LogAzureMapsControlDebug(AzureMapLogEvent.AnimationService_SetCoordinates, "Options", options);

        var animation = new MoveAlongRouteAnimation(Guid.NewGuid().ToString(), jsRuntime);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), animation.Id, points, null, pin.Id, options).ConfigureAwait(false);
        return animation;
    }
}
