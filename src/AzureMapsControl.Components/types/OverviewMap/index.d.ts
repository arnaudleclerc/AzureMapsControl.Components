/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
import * as azmaps from 'azure-maps-control';

declare namespace atlas {

    export module control {

        /** A control that displays an overview map of the area the main map is focused on. */
        export class OverviewMap implements azmaps.Control {

            /****************************
             * Constructor
             ***************************/

            /**
             * A control that displays an overview map of the area the main map is focused on
             * @param options Options for defining how the control is rendered and functions.
             */
            constructor(options?: OverviewMapOptions);

            /****************************
             * Public Methods
             ***************************/

            /** Dispose the overview map control and clean up its resources. */
            public dispose(): void;

            /**
             * Get the overview map instance. Use this to get the map and customize its settings.
             */
            public getOverviewMap(): azmaps.Map;

            /**
             * Get the underlying layers used to render the overview area on the map.
             */
            public getLayers(): OverviewMapLayers;

            /**
             * Get the options for the overview map control.
             */
            public getOptions(): OverviewMapOptions;

            /**
             * Set the options for the overview map control.
             * @param options The options to set.
             */
            public setOptions(options: OverviewMapOptions): void;

            /**
             * Action to perform when the control is added to the map.
             * @param map The map the control was added to.
             * @param options The control options used when adding the control to the map.
             * @returns The HTML Element that represents the control.
             */
            public onAdd(map: azmaps.Map, options?: azmaps.ControlOptions): HTMLElement;

            /**
             * Action to perform when control is removed from the map.
             */
            public onRemove(): void;
        }
    }

    /** Layers used in an overview map to render the parent maps viewport area as a polygon. */
    export interface OverviewMapLayers {
        /** Layer that renders the outline of the parent maps viewport. */
        lineLayer?: azmaps.layer.LineLayer;

        /** Layer that renders the area of the parent maps viewport. */
        polygonLayer?: azmaps.layer.PolygonLayer;
    }

    /** Options for the OverviewMapOptions. */
    export interface OverviewMapOptions {

        /**
        * The style of the control. Can be; light, dark, auto, or any CSS3 color. When set to auto, the style will change based on the map style. Default `light`
        */
        style?: azmaps.ControlStyle | string;

        /**
         * The name of the style to use when rendering the map. 
         * Available styles can be found in the [supported styles](https://docs.microsoft.com/en-us/azure/azure-maps/supported-map-styles) article.
         * Default: `road`
         */
        mapStyle?: string;

        /** The number of zoom levels to offset from the parent map zoom level when synchronizing zoom level changes. Default: `-5` */
        zoomOffset?: number;

        /** Zoom level to set on overview map when not synchronizing zoom level changes. Default: `1` */
        zoom?: number;

        /** The width of the overview map in pixels. Default: `150` */
        width?: number;

        /** The height of the overview map in pixels. Default: `150` */
        height?: number;

        /** Specifies if bearing and pitch changes should be synchronized. Default: `true` */
        syncBearingPitch?: boolean;

        /** Specifies if zoom level changes should be synchronized. Default: `true` */
        syncZoom?: boolean;

        /** Specifies if the overview map is interactive. Default: `true` */
        interactive?: boolean;

        /** Specifies if the overview map is minimized or not. Default: `false` */
        minimized?: boolean;

        /** Specifies if a toggle button for minimizing the overview map should be displayed or not. Default: `true` */
        showToggle?: boolean;

        /** 
         * Specifies the type of information to overlay on top of the map.
         * - area: shows a polygon area of the parent map view port.
         * - marker: shows a marker for the center of the parent map.
         * - none: does not display any overlay on top of the overview map.
         * Default: `area`
         */
        overlay?: 'area' | 'marker' | 'none';

        /**
         * Options for customizing the marker overlay. 
         * If the draggable option of the marker it enabled, the map will center over the marker location after it has been dragged to a new location.
         */
        markerOptions?: azmaps.HtmlMarkerOptions;

        /** Specifies the shape of the overview map. Default: `'square`' */
        shape?: 'square' | 'round';

        /** Specifies if the overview map control is visible or not. Default: `true` */
        visible?: boolean;
    }
}

export = atlas;