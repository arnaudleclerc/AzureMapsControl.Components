/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
/* eslint-disable max-classes-per-file */
import * as azmaps from 'azure-maps-control';

declare namespace atlas {

    export module layer {

        /**
         * A layer that renders point data from a data source as HTML elements on the map.
         */
        export class HtmlMarkerLayer extends azmaps.layer.Layer {

            /**
            * Constructs a new HtmlMarkerLayer.
            * @param source The id or instance of a data source which the layer will render.
            * @param id The id of the layer. If not specified a random one will be generated.
            * @param options The options of the Html marker layer.
            */
            constructor(source?: string | azmaps.source.Source, id?: string, options?: HtmlMarkerLayerOptions);

            /**
             * Gets the id of the layer.
             */
            public getId(): string;

            /**
             * Gets the map that the layer is currently added to, or null.
             */
            public getMap(): azmaps.Map;

            /**
            * Gets the options of the Html Marker layer.
            */
            public getOptions(): HtmlMarkerLayerOptions;

            /**
             * Gets the source provided when creating the layer.
             */
            public getSource(): string | azmaps.source.Source;

            /**
            * Sets the options of the Html marker layer.
            * @param options The new options of the Html marker layer.
            */
            public setOptions(options: HtmlMarkerLayerOptions): void;

            /** Force the layer to refresh and update. */
            public update(): void;

            onAdd(map: azmaps.Map, options?: azmaps.ControlOptions): HTMLElement;

            onRemove(): void;
        }
    }

    /**
     * Options for the HTML Marker Layer class.
     */
    export interface HtmlMarkerLayerOptions {
        /**
         * The id or instance of a data source which the layer will render.
         */
        source?: string | azmaps.source.Source;

        /**
         * Required when the source of the layer is a VectorTileSource.
         * A vector source can have multiple layers within it, this identifies which one to render in this layer.
         * Prohibited for all other types of sources.
         */
        sourceLayer?: string;

        /**
         * Specifies if the layer should update while the map is moving. When set to false, rendering in the map view will 
         * occur after the map has finished moving. New data is not rendered until the moveend event fires. When set to true, 
         * the layer will constantly re-render as the map is moving which ensures new data is always rendered right away, 
         * but may reduce overall performance. Default: `false`
         */
        updateWhileMoving?: boolean;

        /**
         * A callback function that generates a HtmlMarker for a given data point.
         * The `id` and `properties` values will be added to the marker as properties within the layer after being created by this callback function.
         */
        markerCallback?: (id: string, position: azmaps.data.Position, properties: any) => azmaps.HtmlMarker;

        /**
         * An expression specifying conditions on source features.
         * Only features that match the filter are displayed.
         * Default: `['==', ['geometry-type'], 'Point']`
         */
        filter?: azmaps.Expression;
        /**
         * An integer specifying the minimum zoom level to render the layer at.
         * This value is inclusive, i.e. the layer will be visible at `maxZoom > zoom >= minZoom`.
         * Default `0`.
         * @default 0
         */
        minZoom?: number;
        /**
         * An integer specifying the maximum zoom level to render the layer at.
         * This value is exclusive, i.e. the layer will be visible at `maxZoom > zoom >= minZoom`.
         * Default `24`.
         * @default 24
         */
        maxZoom?: number;
        /**
         * Specifies if the layer is visible or not.
         * Default `true`.
         * @default true
         */
        visible?: boolean;
    }

    /**
     * A class for creating Pie Charts as HTML Markers on a map.
     */
    export class PieChartMarker extends azmaps.HtmlMarker implements ExtendedHtmlMarker {
        /**
         * Creates an HTML Marker in the shape of a pie chart.
         * @param options Options for rendering the Pie Chart marker.
         */
        constructor(options: PieChartMarkerOptions);

        /**
         * Gets the total value of all slices summed togehter.
         * @returns The total value of all slices summed togehter.
         */
        public getTotalValue(): number;

        /**
         * Gets the value of a slice of the pie based on it's index.
         * @param idx The index of the slice.
         * @returns The value of a slice of the pie based on it's index.
         */
        public getSliceValue(idx: number): number;

        /**
         * Gets the percentage value of a slice of the pie based on it's index. 
         * @param idx The index of the slice.
         * @returns The percentage value of a slice of the pie based on it's index.
         */
        public getSlicePercentage(idx: number): number;

        /**
         * Gets the options of the pie chart marker.
         * @returns The options of the pie chart marker.
         */
        public getOptions(): PieChartMarkerOptions;

        /**
         * Sets the options of the pie chart marker.
         * @param options The options to set on the marker.
         */
        public setOptions(options: PieChartMarkerOptions): void;
    }

    /** An interface for the HtmlMarker class that extends it by adding an `id` and a `properties` property. */
    export interface ExtendedHtmlMarker extends azmaps.HtmlMarker {

        /** ID of the marker. */
        id?: string;

        /** Properties attached to the marker. */
        properties?: any;
    }

    /**
     * Options for styling a PieChartMarker.
     */
    export interface PieChartMarkerOptions {
        /** The value of each slice of the pie. */
        values: number[],

        /** The radius of a pie chart in pixels. Default: `40` */
        radius?: number,

        /** The inner radius of the pie chart in pixels. Default: `0` */
        innerRadius: number;

        /** The colors of each category in the pie chart. Should have a length >= to largest values array in data set. Default: `['#d7191c','#fdae61','#ffffbf','#abdda4','#2b83ba']` */
        colors?: string[],

        /** The color to fill the center of a pie chart when inner radius is greated than 0. Default: `transparent` */
        fillColor?: string,

        /** The stroke width in pixels. Default: `0` */
        strokeWidth?: number,

        /** The color of the stroke line. Default: `#666666` */
        strokeColor?: string,

        /** A CSS class name to append to the `text` tag of the SVG pie chart. */
        textClassName?: string;

        /** A callback handler which defines the value of a tooltip for a slice of the pie. */
        tooltipCallback?: (marker: PieChartMarker, sliceIdx: number) => string;

        /**
         * Indicates the marker's location relative to its position on the map.
         * Optional values: `"center"`, `"top"`, `"bottom"`, `"left"`, `"right"`,
         * `"top-left"`, `"top-right"`, `"bottom-left"`, `"bottom-right"`.
         * Default `"bottom"`
         * @default "bottom"
         */
        anchor?: string;

        /**
         * Indicates if the user can drag the position of the marker using the mouse or touch controls.
         * default `false`
         * @default false
         */
        draggable?: boolean;

        /**
         * An offset in pixels to move the popup relative to the markers center.
         * Negatives indicate left and up.
         * default `[0, -18]`
         * @default [0, -18]
         */
        pixelOffset?: azmaps.Pixel;

        /**
         * The position of the marker.
         * default `[0, 0]`
         * @default [0, 0]
         */
        position?: azmaps.data.Position;

        /**
         * A popup that is attached to the marker.
         */
        popup?: azmaps.Popup;

        /**
         * Specifies if the marker is visible or not.
         * default `true`
         * @default true
         */
        visible?: boolean;

        /**
         * Text to display at the center of the pie chart.
         */
        text?: string;
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
         * Adds an event to the `HtmlMarkerLayer`.
         * @param eventType The event name.
         * @param target The `fullscreenchanged` to add the event for.
         * @param callback The event handler callback.
         */
        add(eventType: 'click' |
            'contextmenu' |
            'dblclick' |
            'drag' |
            'dragstart' |
            'dragend' |
            'keydown' |
            'keypress' |
            'keyup' |
            'mousedown' |
            'mouseenter' |
            'mouseleave' |
            'mousemove' |
            'mouseout' |
            'mouseover' |
            'mouseup',
            target: atlas.layer.HtmlMarkerLayer, callback: (e: azmaps.TargetedEvent) => void): void;

        /**
         * Adds an event to the `HtmlMarkerLayer` once.
         * @param eventType The event name.
         * @param target The `fullscreenchanged` to add the event for.
         * @param callback The event handler callback.
         */
        addOnce(eventType: 'click' |
            'contextmenu' |
            'dblclick' |
            'drag' |
            'dragstart' |
            'dragend' |
            'keydown' |
            'keypress' |
            'keyup' |
            'mousedown' |
            'mouseenter' |
            'mouseleave' |
            'mousemove' |
            'mouseout' |
            'mouseover' |
            'mouseup',
            target: atlas.layer.HtmlMarkerLayer, callback: (e: azmaps.TargetedEvent) => void): void;


        /**
         * Removes an event listener from the `HtmlMarkerLayer`.
         * @param eventType The event name.
         * @param target The `fullscreenchanged` to remove the event for.
         * @param callback The event handler callback.
         */
        remove(eventType: string, target: atlas.layer.HtmlMarkerLayer, callback: (e: azmaps.TargetedEvent) => void): void;
    }
}

export = atlas;