/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
import * as azmaps from 'azure-maps-control';

declare namespace atlas {

    export module control {

        /** A control that displays a scale bar relative to the pixel resolution at the center of the map. */
        export class ScaleBarControl implements azmaps.Control {

            /**
            * AA control that displays a scale bar relative to the pixel resolution at the center of the map.
            * @param options Options for defining how the control is rendered and functions.
            */
            constructor(options?: ScaleBarControlOptions);

            onAdd(map: azmaps.Map, options?: azmaps.ControlOptions): HTMLElement;

            onRemove(): void;
        }
    }

    /** Options for the ScaleBarControl. */
    export interface ScaleBarControlOptions {

        /** The distance units of the scale bar. Default: `'imperial'` */
        units?: 'imperial' | 'metric' | 'meters' | 'kilometers' | 'yards' | 'feet' | 'miles' | 'nauticalMiles';

        /** The maximum length of the scale bar in pixels. Default: `100` */
        maxBarLength?: number;
    }
}

export = atlas;