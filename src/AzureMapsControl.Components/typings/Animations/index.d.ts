/* eslint-disable quotes */
/* eslint-disable @typescript-eslint/ban-types */
/* eslint-disable max-len */
/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
// eslint-disable-next-line max-classes-per-file
import * as azmaps from 'azure-maps-control';

declare namespace atlas {

    /////////////////////////////
    // Animation classes
    /////////////////////////////

    export module animations {

        /** Group animation handler. */
        export class GroupAnimation {

            /**
             * Group animation handler.
             * @param animations Array of animations to handle.
             */
            constructor(animations: (IPlayableAnimation | GroupAnimation)[], options?: GroupAnimationOptions);

            /** Disposes the animation. */
            public dispose(): void;

            /** Gets the duration of the animation. */
            public getDuration(): number;

            /** Gets the animation options. */
            public getOptions(): GroupAnimationOptions;

            /** Checks to see if the animaiton is playing. */
            public isPlaying(): boolean;

            /**
             * Plays the group of animations.
             */
            public play(): void;

            /** 
             * Stops all animations and jumps back to the beginning of each animation.
             */
            public reset(): void;

            /** Stops all animations and jumps to the final state of each animation. */
            public stop(): void;

            /**
             * Sets the options of the animation.
             * @param options Options to apply to the animation.
             */
            public setOptions(options: GroupAnimationOptions): void;
        }

        /**
         * The events supported by the `PlayableAnimation`.
         */
        export interface PlayableAnimationEvents {
            /** Event fired when the animation progress changes. */
            onprogress: PlayableAnimationEvent;

            /** Event fired when the animation has completed. */
            oncomplete: azmaps.TargetedEvent;
        }

        /** An abstract class which defines an animation that supports a play function. */
        export abstract class PlayableAnimation extends azmaps.internal.EventEmitter<PlayableAnimationEvents> implements IPlayableAnimation {
            /** Disposes the animation. */
            public dispose(): void;

            /** Gets the duration of the animation. */
            public getDuration(): number;

            /** Gets the animation options. */
            public getOptions(): PlayableAnimationOptions;

            /**
             * Checks to see if the animaiton is playing.
             */
            public isPlaying(): boolean;

            /** Pauses the animation. */
            public pause(): void

            /**
             * Plays the animation.
             */
            public play(): void;

            /** 
             * Stops the animation and jumps back to the beginning of the animation. 
             */
            public reset(): void;

            /**
             * Advances the animation to specific step. 
             * @param progress The progress of the animation to advance to. A value between 0 and 1.
             */
            public seek(progress: number): void;

            /** 
             * Stops the animation and jumps back to the end of the animation. 
             */
            public stop(): void;

            /** Sets the options of the animation. */
            public setOptions(options: PlayableAnimationOptions): void;

            /** 
             * Callback function that contains the animation frame logic.  
             * @param progress The progress of the animation where 0 is start and 1 is the end.
             * @returns Any frame state information to pass to the onprogress event.
             */
            public abstract onAnimationProgress(progress: number): any;
        }

        /** A class for frame based animations. */
        export class FrameBasedAnimationTimer extends PlayableAnimation {
            /**
             * An class for frame based animations.
             * @param numberOfFrames The number of frames in the animation.
             * @param onFrame A callback function to trigger when the frame index changes.
             * @param options Animation options. 
             */
            constructor(numberOfFrames: number, onFrame: (frameIdx: number) => void, options?: PlayableAnimationOptions);

            /** Gets the current frame index of the animation. Returns -1 if animation hasn't started, or if there is 0 frames. */
            public getCurrentFrameIdx(): number;

            /** Gets the number of frames in the animation. */
            public getNumberOfFrames(): number;

            /**
             * Sets the frame index of the animation.
             * @param frameIdx The frame index to advance to.
             */
            public setFrameIdx(frameIdx: number): void;

            /**
             * Sets the number of frames in the animation.
             * @param numberOfFrames The number of frames in the animation.
             */
            public setNumberOfFrames(numberOfFrames: number): void;

            /** Overriden, do not use. */
            public onAnimationProgress(progress: number): void;
        }

        /**
         * Adds an offset array property to point shapes or HtmlMarkers and animates it's y value to simulate dropping. 
         * Use with a symbol layer with the icon/text offset property set to ['get', 'offset'] and the opacity set to ['get', 'opacity'].
         * @param shapes A one or more point geometries or shapes to drop in. 
         * @param datasource The data source to drop the point shapes into.
         * @param height The height at which to drop the shape from.Default: 200 pixels
         * @param options Options for the animation.
         */
        export function drop(shapes: azmaps.data.Point | azmaps.data.Feature<azmaps.data.Point, any> | azmaps.Shape | (azmaps.data.Point | azmaps.data.Feature<azmaps.data.Point, any> | azmaps.Shape)[], dataSource: azmaps.source.DataSource, height?: number, options?: PlayableAnimationOptions): PlayableAnimation;

        /**
         * Adds an offset to HtmlMarkers to animate it's y value to simulate dropping. Animation modifies `pixelOffset` value of HtmlMarkers. 
         * @param markers A one or more HtmlMarkers to drop in. 
         * @param map The map to drop the markers into.
         * @param height The height at which to drop the shape from. Default: 200 pixels
         * @param options Options for the animation.
         */
        export function dropMarkers(markers: azmaps.HtmlMarker | azmaps.HtmlMarker[], map?: azmaps.Map, height?: number, options?: PlayableAnimationOptions): PlayableAnimation;

        /**
         * Animates the update of coordinates on a shape or HtmlMarker. Shapes will stay the same type. Only base animation options supported for geometries other than Point. 
         * @param shape The shape to animate.
         * @param newCoordinates The new coordinates of the shape. Must be the same dimension as required by the shape or suitable subset will be picked. 
         * @param options Options for the animation.
         */
        export function setCoordinates(shape: azmaps.Shape | azmaps.HtmlMarker, newCoordinates: azmaps.data.Position | azmaps.data.Position[] | azmaps.data.Position[][] | azmaps.data.Position[][][], options?: PathAnimationOptions | MapPathAnimationOptions): PlayableAnimation;

        /**
         * Animates the path of a LineString. 
         * @param shape A LineString shape to animate.
         * @param options Options for the animation.
         */
        export function snakeline(shape: azmaps.Shape, options?: PathAnimationOptions | MapPathAnimationOptions): PlayableAnimation;

        /**
         * Animates a map and/or a Point shape, or marker along a path. 
         * @param path The path to animate the point along. Must be either an array of positions, or a LineString geometry/shape.
         * @param shape A Point shape or marker to animate along the path.
         * @param options Options for the animation.
         */
        export function moveAlongPath(path: azmaps.data.Position[] | azmaps.data.LineString | azmaps.Shape, shape?: azmaps.Shape | azmaps.HtmlMarker, options?: PathAnimationOptions | MapPathAnimationOptions): PlayableAnimation;

        /**
         * Animates a map and/or a Point shape along a route path. The movement will vary based on timestamps within the point feature properties. All points must have a `timestamp` property that is a `Date.getTime()` value. Use the `extractRoutePoints` function to preprocess data.
         * @param route The route path to animate the point along. Each feature must have a `_timestamp` property.
         * @param shape A Point shape to animate.
         * @param options Options for the animation.
         */
        export function moveAlongRoute(route: azmaps.data.Feature<azmaps.data.Point, any>[], shape?: azmaps.Shape | azmaps.HtmlMarker, options?: RoutePathAnimationOptions): RoutePathAnimation;

        /**
         * Extracts points from a shapes or features that form a time based route and sorts them by time. 
         * Timestamps must parsable by the `atlas.math.parseTimestamp` function.
         * All extracted points will have a `_timestamp` property that contains the Date.getTime() value.
         * Features must be a Point, MultiPoint, or LineString and must contain properties that include timestamp information. 
         * If a timestamp property name is not specified, `_timestamp` will be used.
         * If a feature collection is passed in, the first shape with a matching timestamp property will dictate what is extracted. 
         * If the first shape is a Point, all points in the colleciton with the timestamp property will be extracted. 
         * If the first shape is a LineString or MultiLineString;
         * - If it contains a timestamp property with an array of values the same length as coordinates in the feature, new Point features will be created from a combination of the coordinates and timestamp values.
         * - If the feature has a `waypoints` property that contains an array of Point features with the timestamp property and the same number of coordinates, then these p will be extracted.
         * @param shapes The shapes to extract the route points from.
         * @param timestampProperty The name of the property that contains the timestamp for each feature. If not specified, defaults to `_timestamp`.
         */
        export function extractRoutePoints(shapes: azmaps.data.FeatureCollection | azmaps.Shape | azmaps.data.Feature<azmaps.data.Geometry, any> | (azmaps.Shape | azmaps.data.Feature<azmaps.data.Geometry, any>)[], timestampProperty?: string): azmaps.data.Feature<azmaps.data.Point, any>[];

        /**
         * Animates the dash-array of a line layer to make it appear to flow. 
         * The lineCap option of the layer must not be 'round'. If it is, it will be changed to 'butt'.
         * @param layer The layer to animate.
         * @param options Animation options.
         */
        export function flowingDashedLine(layer: azmaps.layer.LineLayer, options?: MovingDashLineOptions): IPlayableAnimation;

        /**
         * Animates the morphing of a shape from one geometry type or set of coordinates to another.
         * @param shape The shape to animate.
         * @param newGeometry The new geometry to turn the shape into.
         * @param options Options for the animation.
         */
        export function morph(shape: azmaps.Shape, newGeometry: azmaps.data.Geometry, options?: PlayableAnimationOptions): PlayableAnimation;

        /**
         * Creates a playable animation delay. This is useful for group animations. 
         * @param timeout The time, in milliseconds (thousandths of a second), to delay before reaching the end of the animation.
         */
        export function delay(timeout: number, callback?: string | Function): IPlayableAnimation;

        /**
         * Creates a playable animation that triggers a callback function on constant interval.  
         * @param period The interval time between calls to the callback.
         * @param callback The callback function to trigger on each interval.
         * @param numberOfIntervals The number of intervals to trigger in the animation. DEfault: Infinity
         */
        export function interval(period: number, callback?: string | Function, numberOfIntervals?: number): IPlayableAnimation;

        /**
         * A version of the setInterval function based on requestAnimationFrame.
         * @param callback The callback function to trigger on each interval.
         * @param timeout The time, in milliseconds (thousandths of a second), the timer should delay in between executions of the specified callback function
         */
        export function setInterval(callback: string | Function, timeout: number): number;

        /**
         * Disposes a setInterval instance.
         * @param intervalId The ID from the creation of a setInterval.
         */
        export function clearInterval(intervalId: number): void;

        /**
         * A version of the setTimeout function based on requestAnimationFrame.
         * @param callback The callback function to trigger after a period of time.
         * @param timeout The time, in milliseconds (thousandths of a second), the timer should delay before executioning the specified callback function
         */
        export function setTimeout(callback: string | Function, timeout: number): number;

        /**
         * Disposes a setTimeout instance.
         * @param timeoutId The ID of the setTimeout instance.
         */
        export function clearTimeout(timeoutId: number): void;

        /**
         * Retrieves an easing function by name, or null if a matching easing function is not found.
         * @param easing Name of the easing function to retrieve.
         */
        export function getEasingFn(easing: string): (progress: number) => number;

        /**
         * Retrieves the name of all the built in easing functions.
         */
        export function getEasingNames(): string[];
    }

    export module layer {
        /** A layer that can smoothly animate through an array of tile layers. */
        export class AnimatedTileLayer extends azmaps.layer.Layer implements IPlayableAnimation {
            /**
             * A layer that can smoothly animate through an array of tile layers.
             * @param options Options for the layer.
             */
            constructor(options?: AnimatedTileLayerOptions);

            /** Disposes the layer. */
            public dispose(): void;

            /** Gets the duration of the animation. */
            public getDuration(): number;

            /** Gets the options for the layer. */
            public getOptions(): AnimatedTileLayerOptions;

            /** Gets the underlying frame based animation instance. */
            public getPlayableAnimation(): animations.PlayableAnimation;

            /** Checks to see if the animaiton is playing. */
            public isPlaying(): boolean;

            /** Pauses the animation. */
            public pause(): void;

            /** Plays the animation. */
            public play(): void;

            /** Stops the animation and jumps back to the beginning the animation. */
            public reset(): void;

            /** Stopes the animation. */
            public stop(): void;

            /**
             * Sets the frame index of the animation.
             * @param frameIdx The frame index to advance to.
             */
            public setFrameIdx(frmeIdx: number): void;

            /** 
             * Sets the options of the layer.
             * @param options The options to apply to the layer.
             */
            public setOptions(options: AnimatedTileLayerOptions): void;
        }
    }

    /////////////////////////////
    // Interfaces
    /////////////////////////////

    /** Options for a group of animations. */
    export interface GroupAnimationOptions {
        /** How to play the animations. Default: 'playType' */
        playType: 'together' | 'sequential' | 'interval';

        /** If the `playType` is set to `interval`, this option specifies the time interval to start each animation in milliseconds. Default: `100` */
        interval?: number;

        /** Specifies if the animation should start automatically or wait for the play function to be called. Default: false */
        autoPlay?: boolean;
    }

    /** Playable animation event argument. */
    export interface PlayableAnimationEvent {
        /** The event type. */
        type: string;

        /** The animation the event occurered on. */
        animation: animations.PlayableAnimation;

        /** Progress of the animation where 0 is the start and 1 is the end. */
        progress: number;

        /** The progress of the animation after being passed through an easing function. */
        easingProgress: number;

        /** The focal position of an animation frame. Returned by path animations.  */
        position?: azmaps.data.Position;

        /** The focal heading of an animation frame. Returned by path animations. */
        heading?: number;
    }

    /** Event arguments for a frame based animation. */
    export interface FrameBasedAnimationEvent {
        /** The event type. */
        type: string;

        /** The animation the event occurered on. */
        animation: animations.FrameBasedAnimationTimer;

        /** The index of the frame if using the frame based animation timer. */
        frameIdx?: number;

        /** The number of frames in the animation. */
        numFrames?: number;
    }

    /** Event arguments for a RoutePathAnimation state. */
    export interface RoutePathAnimationEvent extends PlayableAnimationEvent {
        /** The current position on the path. */
        position?: azmaps.data.Position;

        /** The current heading on the path. */
        heading?: number;

        /** Average speed between points in meters per second. */
        speed?: number;

        /** Estimated timestamp in the animation based on the timestamp information provided for each point.  */
        timestamp?: number;
    }

    /** Base animation options. */
    export interface PlayableAnimationOptions {
        /** The duration of the animation in ms. Default: 1000 ms */
        duration?: number;

        /** Specifies if the animation should start automatically or wait for the play function to be called. Default: false */
        autoPlay?: boolean;

        /** The easing of the animaiton. Default: linear */
        easing?: string | ((progress: number) => number);

        /** Specifies if the animation should loop infinitely. Default: false */
        loop?: boolean;

        /** Specifies if the animation should play backwards. Default: false */
        reverse?: boolean;

        /** A multiplier of the duration to speed up or down the animation. Default: 1 */
        speedMultiplier?: number;

        /** Specifies if the animation should dispose itself once it has completed. Default: false */
        disposeOnComplete?: boolean;
    }

    export interface MapPathAnimationOptions extends PathAnimationOptions {
        /** Map to animation along path. */
        map?: azmaps.Map;

        /** A fixed zoom level to snap the map to on each animation frame. By default the maps current zoom level is used. */
        zoom?: number;

        /** A pitch value to set on the map. By default this is not set. */
        pitch?: number;

        /** Specifies if the map should rotate such that the bearing of the map faces the direction the map is moving. Default: true */
        rotate?: boolean;

        /** When rotate is set to true, the animation will follow the animation. An offset of 180 will cause the camera to lead the animation and look back. Default: 0 */
        rotationOffset?: number;
    }

    export interface PathAnimationOptions extends PlayableAnimationOptions {
        /** Specifies if a curved geodesic path should be used between points rather than a straight pixel path. Default: false */
        geodesic?: boolean;

        /** Specifies if metadata should be captured as properties of the shape. Potential metadata properties that may be captured: _heading */
        captureMetadata?: boolean;
    }

    /** Animates a map and/or a Point shape along a route path. The movement will vary based on timestamps within the point feature properties. */
    export interface RoutePathAnimation {
        /** Disposes the animation. */
        dispose(): void;

        /** Gets the duration of the animation. Returns Infinity if the animations loops forever. */
        getDuration(): number;

        /** Gets the animation options. */
        getOptions(): RoutePathAnimationOptions;

        /** Sets the options of the animation. */
        setOptions(options: RoutePathAnimationOptions): void;

        /** Gets the time span of the animation. */
        getTimeSpan(): TimeSpan;

        /** Gets the positions that form the route path. */
        getPath(): azmaps.data.Position[];
    }

    /** Options for animating the map along a path. */
    export interface RoutePathAnimationOptions {
        /** Interpolation calculations to perform on property values between points during the animation. Requires `captureMetadata` to be enabled. */
        valueInterpolations?: PointPairValueInterpolation[];

        /** Specifies if metadata should be captured as properties of the shape. Potential metadata properties that may be captured: heading, speed, timestamp */
        captureMetadata?: boolean;

        /** Map to animation along path. */
        map?: azmaps.Map;

        /** A fixed zoom level to snap the map to on each animation frame. By default the maps current zoom level is used. */
        zoom?: number;

        /** A pitch value to set on the map. By default this is not set. */
        pitch?: number;

        /** Specifies if the map should rotate such that the bearing of the map faces the direction the map is moving. Default: true */
        rotate?: boolean;

        /** When rotate is set to true, the animation will follow the animation. An offset of 180 will cause the camera to lead the animation and look back. Default: 0 */
        rotationOffset?: number;

        /** Specifies if the animation should start automatically or wait for the play function to be called. Default: false */
        autoPlay?: boolean;

        /** Specifies if the animation should loop infinitely. Default: false */
        loop?: boolean;

        /** Specifies if the animation should play backwards. Default: false */
        reverse?: boolean;

        /** A multiplier of the duration to speed up or down the animation. Default: 1 */
        speedMultiplier?: number;
    }

    /** An interface that all playable animations adhere to. */
    export interface IPlayableAnimation {

        /** Disposes the animation. */
        dispose(): void;

        /** Gets the duration of the animation. Returns Infinity if the animations loops forever. */
        getDuration(): number;

        /** Checks to see if the animaiton is playing.  */
        isPlaying(): boolean;

        /** Pauses the animation. */
        pause(): void;

        /** Plays the animation. */
        play(): void;

        /** Reset the animation. */
        reset(): void;

        /** Stops the animation. */
        stop(): void;
    }

    /** An object that defines the options for an AnimatedTileLayer. */
    export interface AnimatedTileLayerOptions extends PlayableAnimationOptions {
        /** The array of tile layers options to animate through. fadeDuration and visible options are ignored. */
        tileLayerOptions?: azmaps.TileLayerOptions[];

        /** A boolean specifying if the animated tile layer is visible or not. Default: true */
        visible?: boolean;
    }

    /**
     * Details about a period time.
     */
    export interface TimeSpan {
        /** The start of the time span. Can be a number representing a date/time, or a number representing an order. */
        begin: number;

        /** The end of the time span. Can be a number representing a date/time, or a number representing an order. */
        end: number;

        /** The difference between the begin and end values. */
        length: number;
    }

    /** Defines how the value of property in two points is extrapolated. */
    export interface PointPairValueInterpolation {
        /**
         * How the interpolation is performed. Certain interpolations require the data to be a certain value.
         * - `linear`,`min`, `max`, `avg`: `number` or `Date`
         * - `nearest`: `any`
         * Default: `linear`
         */
        interpolation: 'linear' | 'nearest' | 'min' | 'max' | 'avg';

        /**
         * The path to the property with each sub-property separated with a forward slash "/", for example "property/subproperty1/subproperty2".
         * Array indices can be added as subproperties as well, for example "property/0".
         */
        propertyPath: string;
    }

    export interface MovingDashLineOptions {
        /** The length of the dashed part of the line. Default: 4 */
        dashLength: number;

        /** The length of the gap part of the line. Default: 4 */
        gapLength: number;

        /** Specifies if the animation should start automatically or wait for the play function to be called. Default: false */
        autoPlay?: boolean;

        /** The easing of the animaiton. Default: linear */
        easing?: string | ((progress: number) => number);

        /** Specifies if the animation should loop infinitely. Default: false */
        loop?: boolean;

        /** Specifies if the animation should play backwards. Default: false */
        reverse?: boolean;

        /** A multiplier of the duration to speed up or down the animation. Default: 1 */
        speedMultiplier?: number;
    }
}

/**
 * This module partially defines the map control.
 * This definition only includes the features added by using the drawing tools.
 * For the base definition see:
 * https://docs.microsoft.com/javascript/api/azure-maps-control/?view=azure-maps-typescript-latest
 */
declare module "azure-maps-control" {
    /**
     * This interface partially defines the map control's `EventManager`.
     * This definition only includes the method added by using the drawing tools.
     * For the base definition see:
     * https://docs.microsoft.com/javascript/api/azure-maps-control/atlas.eventmanager?view=azure-maps-typescript-latest
     */
    export interface EventManager {
        /**
                * Adds an event to the `PlayableAnimation`.
                * @param eventType The event name.
                * @param target The `PlayableAnimation` to add the event for.
                * @param callback The event handler callback.
                */
        add(eventType: "onprogress", target: atlas.animations.PlayableAnimation, callback: (e: atlas.PlayableAnimationEvent) => void): void;

        /**
         * Adds an event to the `PlayableAnimation`.
         * @param eventType The event name.
         * @param target The `PlayableAnimation` to add the event for.
         * @param callback The event handler callback.
         */
        add(eventType: "oncomplete", target: atlas.animations.PlayableAnimation, callback: (e: atlas.PlayableAnimationEvent) => void): void;

        /**
         * Adds an event to the `FrameBasedAnimationTimer`.
         * @param eventType The event name.
         * @param target The `FrameBasedAnimationTimer` to add the event for.
         * @param callback The event handler callback.
         */
        add(eventType: "onframe", target: atlas.animations.FrameBasedAnimationTimer, callback: (e: atlas.FrameBasedAnimationEvent) => void): void;

        /**
         * Adds an event to the `PlayableAnimation` once.
         * @param eventType The event name.
         * @param target The `PlayableAnimation` to add the event for.
         * @param callback The event handler callback.
         */
        addOnce(eventType: "onprogress", target: atlas.animations.PlayableAnimation, callback: (e: atlas.PlayableAnimationEvent) => void): void;

        /**
         * Adds an event to the `PlayableAnimation` once.
         * @param eventType The event name.
         * @param target The `PlayableAnimation` to add the event for.
         * @param callback The event handler callback.
         */
        addOnce(eventType: "oncomplete", target: atlas.animations.PlayableAnimation, callback: (e: atlas.PlayableAnimationEvent) => void): void;

        /**
         * Adds an event to the `FrameBasedAnimationTimer` once.
         * @param eventType The event name.
         * @param target The `FrameBasedAnimationTimer` to add the event for.
         * @param callback The event handler callback.
         */
        addOnce(eventType: "onframe", target: atlas.animations.FrameBasedAnimationTimer, callback: (e: atlas.FrameBasedAnimationEvent) => void): void;

        /**
         * Removes an event listener from the `PlayableAnimation`.
         * @param eventType The event name.
         * @param target The `PlayableAnimation` to remove the event for.
         * @param callback The event handler callback.
         */
        remove(eventType: string, target: atlas.animations.PlayableAnimation, callback: (e?: any) => void): void;

        /**
         * Removes an event listener from the `FrameBasedAnimationTimer`.
         * @param eventType The event name.
         * @param target The `FrameBasedAnimationTimer` to remove the event for.
         * @param callback The event handler callback.
         */
        remove(eventType: string, target: atlas.animations.FrameBasedAnimationTimer, callback: (e?: any) => void): void;
    }
}

export = atlas;