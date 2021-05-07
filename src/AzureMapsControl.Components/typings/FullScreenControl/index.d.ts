/* eslint-disable @typescript-eslint/member-ordering */
/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
import * as azmaps from 'azure-maps-control';

declare namespace atlas {

    export module control {
        /** The events supported by the `FullscreenControl`. */
        export interface FullscreenControlEvents {
            /** Event fired when the fullscreen state changed. Returns a boolean indicating if the container is fullscreen or not. */
            fullscreenchanged: boolean;
        }

        /** A control that toggles the map or a specific container from its defined size to a fullscreen size. */
        export class FullscreenControl extends azmaps.internal.EventEmitter<FullscreenControlEvents> implements azmaps.Control {

            /**
             * A control that toggles the map or a specific container from its defined size to a fullscreen size.
             * @param options Options for defining how the control is rendered and functions.
             */
            constructor(options?: FullscreenControlOptions);

            /** Disposes the control. */
            public dispose(): void;

            /** Gets the options of the control. */
            public getOptions(): FullscreenControlOptions;

            /**
             * Sets the options of the control. 
             * @param options The options.
             */
            public setOptions(options: FullscreenControlOptions): void;

            /** Checks if the map or specified container is in fullscreen mode or not. */
            public isFullscreen(): boolean;

            /** Checks to see if the browser supports going into fullscreen mode. */
            public static isSupported(): boolean;

            onAdd(map: azmaps.Map, options?: azmaps.ControlOptions): HTMLElement;

            onRemove(): void;
        }
    }

    /** Options for the FullscreenControlOptions. */
    export interface FullscreenControlOptions {

        /**
        * The style of the control. Can be; light, dark, auto, or any CSS3 color. When set to auto, the style will change based on the map style.
        * Overridden if device is in high contrast mode.
        * @default light
        */
        style?: azmaps.ControlStyle | string;

        /**
         * Specifies if the control should be hidden if fullscreen is not supported by the browser. 
         * @default false
         */
        hideIfUnsupported?: boolean;

        /**
         * The HTML element that should be made fullscreen. If not specified, the map container element will be used. 
         * If a string is passed in, it will first be used with `document.getElementById` and if null, will then use `document.querySelector`.
         */
        container?: string | HTMLElement;
    }
}

/**
 * This module partially defines the map control.
 * This definition only includes the features added by using the drawing tools.
 * For the base definition see:
 * https://docs.microsoft.com/javascript/api/azure-maps-control/?view=azure-maps-typescript-latest
 */
declare module 'azure-maps-control' {
    /**
     * This interface partially defines the map control's `EventManager`.
     * This definition only includes the method added by using the drawing tools.
     * For the base definition see:
     * https://docs.microsoft.com/javascript/api/azure-maps-control/atlas.eventmanager?view=azure-maps-typescript-latest
     */
    export interface EventManager {
        /**
         * Adds an event to the `fullscreenchanged`.
         * @param eventType The event name.
         * @param target The `fullscreenchanged` to add the event for.
         * @param callback The event handler callback.
         */
        add(eventType: 'fullscreenchanged', target: atlas.control.FullscreenControl, callback: (e: boolean) => void): void;

        /**
         * Adds an event to the `fullscreenchanged` once.
         * @param eventType The event name.
         * @param target The `fullscreenchanged` to add the event for.
         * @param callback The event handler callback.
         */
        addOnce(eventType: 'fullscreenchanged', target: atlas.control.FullscreenControl, callback: (e: boolean) => void): void;


        /**
         * Removes an event listener from the `fullscreenchanged`.
         * @param eventType The event name.
         * @param target The `fullscreenchanged` to remove the event for.
         * @param callback The event handler callback.
         */
        remove(eventType: string, target: atlas.control.FullscreenControl, callback: (e: boolean) => void): void;
    }
}

export = atlas;