import * as azmaps from 'azure-maps-control';
import * as azanimations from 'azure-maps-control-animations';
import { Core } from '../core';
import { EventHelper } from '../events';
import { Geometry, GeometryBuilder, RoutePoint } from '../geometries';
import { HtmlMarkerEventArgs, HtmlMarkerOptions } from '../html-markers';

export class Animation {

    private static readonly _animations = new Map<string, azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation | azanimations.RoutePathAnimation>();

    public static drop(animationId: string,
        shapes: Geometry[],
        datasourceId: string,
        height: number,
        options: azanimations.PlayableAnimationOptions): void {

        const map = Core.getMap();
        const source = map.sources.getById(datasourceId) as azmaps.source.DataSource;

        const animation = azanimations.animations.drop(shapes, source, height, options);

        if (!options.disposeOnComplete) {
            this._animations.set(animationId, animation);
        }
    }

    public static dropMarkers(animationId: string,
        markerOptions: HtmlMarkerOptions[],
        height: number,
        options: azanimations.PlayableAnimationOptions,
        eventInvokeHelper: EventHelper<HtmlMarkerEventArgs>): void {
        const map = Core.getMap();
        const markers: azmaps.HtmlMarker[] = [];
        markerOptions.forEach(markerOption => {
            const marker = Core.getHtmlMarkerFromOptions(markerOption);
            markers.push(marker);
            if (markerOption.events) {
                Core.attachEventsToHtmlMarker(marker, markerOption.events, eventInvokeHelper);
            }
        });

        const animation = azanimations.animations.dropMarkers(markers, map, height, options);

        if (!options.disposeOnComplete) {
            this._animations.set(animationId, animation);
        }
    }

    public static setCoordinates(animationId: string,
        shapeId: string,
        datasourceId: string,
        newCoordinates: azmaps.data.Position | azmaps.data.Position[] | azmaps.data.Position[][] | azmaps.data.Position[][][],
        options: azanimations.PathAnimationOptions | azanimations.MapPathAnimationOptions): void {

        const map = Core.getMap();

        const shape = datasourceId ?
            (map.sources.getById(datasourceId) as azmaps.source.DataSource).getShapeById(shapeId)
            : map.markers.getMarkers().find(marker => (marker as any).amc && (marker as any).amc.id === shapeId);

        const animation = azanimations.animations.setCoordinates(shape, newCoordinates, options);
        if (!options.disposeOnComplete) {
            this._animations.set(animationId, animation);
        }
    }

    public static snakeline(animationId: string,
        lineId: string,
        dataSourceId: string,
        options: azanimations.PathAnimationOptions | azanimations.MapPathAnimationOptions): void {
        const source = Core.getMap().sources.getById(dataSourceId) as azmaps.source.DataSource;
        const shape = source.getShapeById(lineId);
        const animation = azanimations.animations.snakeline(shape, options);

        if (!options.disposeOnComplete) {
            this._animations.set(animationId, animation);
        }
    }

    public static moveAlongPath(animationId: string,
        line: string | azmaps.data.Position[],
        lineSourceId: string,
        pinId: string,
        pinSourceId: string,
        options: azanimations.PathAnimationOptions | azanimations.MapPathAnimationOptions): void {
        const map = Core.getMap();

        let path: azmaps.data.Position[] | azmaps.data.LineString | azmaps.Shape = null;
        if (typeof line === 'string') {
            const lineSource = map.sources.getById(lineSourceId) as azmaps.source.DataSource;
            path = lineSource.getShapeById(line);
        } else {
            path = line;
        }

        let shape: azmaps.Shape | azmaps.HtmlMarker = null;
        if (pinSourceId) {
            const pinSource = map.sources.getById(pinSourceId) as azmaps.source.DataSource;
            shape = pinSource.getShapeById(pinId);
        } else {
            shape = map.markers.getMarkers().find(marker => (marker as any).amc.id === pinId);
        }

        const animation = azanimations.animations.moveAlongPath(path, shape, options);

        if (!options.disposeOnComplete) {
            this._animations.set(animationId, animation);
        }
    }

    public static moveAlongRoute(animationId: string,
        routePoints: RoutePoint[],
        pinSourceId: string,
        pinId: string,
        options: azanimations.RoutePathAnimationOptions): void {
        const map = Core.getMap();

        let shape: azmaps.Shape | azmaps.HtmlMarker = null;
        if (pinSourceId) {
            const pinSource = map.sources.getById(pinSourceId) as azmaps.source.DataSource;
            shape = pinSource.getShapeById(pinId);
        } else {
            shape = map.markers.getMarkers().find(marker => (marker as any).amc.id === pinId);
        }

        const route = routePoints.map(routePoint => {
            const point = GeometryBuilder.buildPoint(routePoint);
            return new azmaps.data.Feature(point, { _timestamp: new Date(routePoint.timestamp).getTime() });
        });

        this._animations.set(animationId, azanimations.animations.moveAlongRoute(route, shape, options));
    }

    public static flowingDashedLine(animationId: string,
        lineLayerId: string,
        options: azanimations.MovingDashLineOptions): void {

        const layer = Core.getMap().layers.getLayerById(lineLayerId) as azmaps.layer.LineLayer;

        this._animations.set(
            animationId,
            azanimations.animations.flowingDashedLine(layer, options)
        );
    }

    public static morph(animationId: string,
        shapeId: string,
        datasourceId: string,
        newGeometry: Geometry,
        options: azanimations.PlayableAnimationOptions): void {

        const map = Core.getMap();
        const shape = (map.sources.getById(datasourceId) as azmaps.source.DataSource).getShapeById(shapeId);

        const animation = azanimations.animations.morph(
            shape,
            GeometryBuilder.buildGeometry(newGeometry),
            options
        );

        if (!options.disposeOnComplete) {
            this, this._animations.set(animationId, animation);
        }
    }

    public static groupAnimations(groupAnimationId: string,
        animationsIds: string[],
        options: azanimations.GroupAnimationOptions): void {

        const animations: (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation)[] = [];
        animationsIds.forEach(animationId => {
            if (this._animations.has(animationId)) {
                const animation = this._animations.get(animationId);
                if (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation) !== null) {
                    animations.push(animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation));
                }
            }
        });
        this._animations.set(
            groupAnimationId,
            new azanimations.animations.GroupAnimation(animations, options)
        );
    }

    public static dispose(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.dispose();
            this._animations.delete(animationId);
        }
    }

    public static pause(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            if (animation as azanimations.IPlayableAnimation !== null) {
                (animation as azanimations.IPlayableAnimation).pause();
            }
        }
    }

    public static play(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            if (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation) !== null) {
                (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation)).play();
            }
        }
    }

    public static reset(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            if (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation) !== null) {
                (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation)).reset();
            }
        }
    }

    public static seek(animationId: string, progress: number): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            if (animation as azanimations.animations.PlayableAnimation !== null) {
                (animation as azanimations.animations.PlayableAnimation).seek(progress);
            }
        }
    }

    public static stop(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            if (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation) !== null) {
                (animation as (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation)).stop();
            }
        }
    }

    public static setOptions(animationId: string, options: azanimations.PlayableAnimationOptions): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            if (animation as azanimations.animations.PlayableAnimation !== null) {
                (animation as azanimations.animations.PlayableAnimation).setOptions(options);
            }
        }
    }

}