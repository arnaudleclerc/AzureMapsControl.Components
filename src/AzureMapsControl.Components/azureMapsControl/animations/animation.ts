import * as azmaps from 'azure-maps-control';
import * as azanimations from 'azure-maps-control-animations';
import { Core } from '../core';
import { EventHelper } from '../events';
import { Geometry } from '../geometries';
import { HtmlMarkerEventArgs, HtmlMarkerOptions } from '../html-markers';

export class Animation {

    private static readonly _animations = new Map<string, azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation>();

    public static snakeline(animationId: string,
        lineId: string,
        dataSourceId: string,
        options: azanimations.PathAnimationOptions | azanimations.MapPathAnimationOptions): void {
        const source = Core.getMap().sources.getById(dataSourceId) as azmaps.source.DataSource;
        const shape = source.getShapeById(lineId);
        this._animations.set(
            animationId,
            azanimations.animations.snakeline(shape, options)
        );
    }

    public static moveAlongPath(animationId: string,
        line: string | azmaps.data.Position[],
        lineSourceId: string,
        pin: string,
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
            shape = pinSource.getShapeById(pin);
        } else {
            shape = map.markers.getMarkers().find(marker => (marker as any).amc.id === pin);
        }

        this._animations.set(
            animationId,
            azanimations.animations.moveAlongPath(path, shape, options)
        );
    }

    public static flowingDashedLine(animationId: string,
        lineLayerId: string,
        options: azanimations.MovingDashLineOptions): void {

        const layer = Core.getMap().layers.getLayerById(lineLayerId) as azmaps.layer.LineLayer;
        this._animations.set(
            animationId,
            azanimations.animations.flowingDashedLine(layer, options)
        )
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

        this._animations.set(
            animationId,
            azanimations.animations.dropMarkers(markers, map, height, options)
        );
    }

    public static groupAnimations(groupAnimationId: string,
        animationsIds: string[],
        options: azanimations.GroupAnimationOptions): void {

        const animations: (azanimations.IPlayableAnimation | azanimations.animations.GroupAnimation)[] = [];
        animationsIds.forEach(animationId => {
            if (this._animations.has(animationId)) {
                animations.push(this._animations.get(animationId));
            }
        });
        this._animations.set(
            groupAnimationId,
            new azanimations.animations.GroupAnimation(animations, options)
        );
    }

    public static drop(animationId: string,
        shapes: Geometry[],
        datasourceId: string,
        height: number,
        options: azanimations.PlayableAnimationOptions): void {

        const map = Core.getMap();
        const source = map.sources.getById(datasourceId) as azmaps.source.DataSource;

        this._animations.set(
            animationId,
            azanimations.animations.drop(shapes, source, height, options)
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
            animation.play();
        }
    }

    public static reset(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.reset();
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
            animation.stop();
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