/* eslint-disable @typescript-eslint/naming-convention */
/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
import * as azmaps from 'azure-maps-control';

declare namespace atlas {

    export module source {
        /**
         * A data source for aggregating point features into cells of a grid system. 
         * Point features will be extracted from atlas.Shape objects, but this shape will not be data bound.
         */
        export class GriddedDataSource extends azmaps.source.Source {
            /**
            * A data source class that makes it easy to manage shapes data that will be displayed on the map.
            * A data source must be added to a layer before it is visible on the map.
            * The `DataSource` class may be used with the `SymbolLayer`, `LineLayer`, `PolygonLayer`, `BubbleLayer`, and `HeatMapLayer`.
            * @param id a unique id that the user assigns to the data source. If this is not specified, then the data source will automatically be assigned an id.
            * @param options the options for the data source.
            */
            constructor(id?: string, options?: GriddedDataSourceOptions);

            /***********************************
             * Public functions
             ***********************************/

            /**
             * Adds points to the data source.
             * @param points The points to add to the data source.
             */
            public add(points: azmaps.data.FeatureCollection | azmaps.data.Feature<azmaps.data.Point, any> | azmaps.data.Point | azmaps.Shape | Array<azmaps.data.Feature<azmaps.data.Point, any> | azmaps.data.Point | azmaps.Shape>): void;

            /**
             * Removes all data in the data source.
             */
            public clear(): void;

            /**
             * Cleans up any resources this data source is consuming.
             */
            public dispose(): void;

            /**
             * Gets all points that are within the specified grid cell.
             * @param cell_id The grid cell id.
             */
            public getCellChildren(cell_id: string): azmaps.data.Feature<azmaps.data.Point, any>[];

            /**
             * Gets all grid cell polygons as a GeoJSON FeatureCollection.
             */
            public getGridCells(): azmaps.data.FeatureCollection;

            /** Gets the ID of the data source. */
            public getId(): string;

            /**
             * Gets the options used by the data source.
             */
            public getOptions(): GriddedDataSourceOptions;

            /**
             * Gets all points as a GeoJSON FeatureCollection.
             */
            public getPoints(): azmaps.data.FeatureCollection;

            /**
             * Downloads a GeoJSON document and imports its data into the data source.
             * The GeoJSON document must be on the same domain or accessible using CORS.
             * @param url The URL to the GeoJSON document.
             */
            public importDataFromUrl(url: string): Promise<void>;

            /**
             * Removes one or more points from the data source.
             * If a string is passed in, it is assumed to be an id.
             * If a number is passed in, removes the point at that index.
             * @param point The point(s), point id(s), or feature(s) to be removed
             */
            public remove(point: number | string | azmaps.data.Feature<azmaps.data.Point, any> | azmaps.Shape | Array<number | string | azmaps.data.Feature<azmaps.data.Point, any>> | azmaps.Shape): void;

            /**
             * Removes one or more points from the datasource based on its id.
             * @param shape shape id
             */
            public removeById(id: number | string | Array<number | string>): void;

            /**
             * Sets the data source options.
             * The data source will retain its current values for any option not specified in the supplied options.
             * @param options The options to be set.
             */
            public setOptions(options: GriddedDataSourceOptions): void;

            /**
             * Overwrites all points in the data source with the new array of points.
             * @param points The new points to add.
             */
            public setPoints(points: azmaps.data.FeatureCollection | Array<azmaps.data.Feature<azmaps.data.Point, any> | azmaps.data.Point | azmaps.Shape>): void;
        }
    }

    /**
    * Specifies how data is rendered wintin a grid system.
    */
    export enum GridType {
        /* Renders data within a square grid as circles. */
        circle = 'circle',

        /* Renders data within a hexagons grid. */
        hexagon = 'hexagon',

        /* Renders data within a hexagon grid as circles. */
        hexCircle = 'hexCircle',

        /* Renders data within a rotate hexagon grid. */
        pointyHexagon = 'pointyHexagon',

        /* Renders data within a square grid. */
        square = 'square',

        /** Renders data within a triangular grid. */
        triangle = 'triangle'
    }

    /**
     * Options for a gridded data source.
     */
    export interface GriddedDataSourceOptions {

        /*
        * Defines custom properties that are calculated using expressions against all the points within each grid cell and added to the properties of each grid cell polygon.
        */
        aggregateProperties?: Record<string, azmaps.AggregateExpression>;

        /* The shape of the data bin to generate. Default: `hexagon` */
        gridType?: GridType;

        /*
        * The spatial width of each cell in the grid in the specified distance units. Default: `25000`
        */
        cellWidth?: number;

        /** The minimium cell width to use by the coverage and scaling operations. Will be snapped to the `cellWidth` if greater than that value. Default: `0` */
        minCellWidth?: number;

        /* The distance units of the cellWidth option. Default: `meters` */
        distanceUnits?: azmaps.math.DistanceUnits;

        /**
         * Maximum zoom level at which to create vector tiles (higher means greater detail at high zoom levels). Default: `18`
         */
        maxZoom?: number;

        /**
         * The aggregate property to calculate the min/max values over the whole data set. Can be an aggregate property or `point_count`.
         */
        scaleProperty?: string;

        /**
        * A data driven expression that customizes how the scaling function is done. This expression has access to the properties of the cell (CellInfo) and the following two properties; 
        * `min` - The minimium aggregate value across all cells in the data source.
        * `max` - The maximium aggregate value across all cells in the data source.
        * A linear scaling function based on the "point_count" property is used by default `scale = (point_count - min)/(max - min)`. 
        * Default: `['/', ['-', ['get', 'point_count'], ['get', 'min']], ['-',  ['get', 'max'], ['get', 'min']]]`
        */
        scaleExpression?: azmaps.Expression;

        /** 
         * A number between 0 and 1 that specifies how much area a cell polygon should consume within the grid cell. 
         * This applies a multiplier to the scale of all cells. If `scaleProperty` is specified, this will add additional scaling. 
         * Default: `1`
         */
        coverage?: number;

        /**
         * The latitude value used to calculate the pixel equivalent of the cellWidth. Default: `0`
         */
        centerLatitude?: number;
    }

    /** An object that represents a range of values. */
    export interface ScaleRange {
        /** The minimum range value. */
        min: number;

        /** The maximum range value. */
        max: number;
    }

    /**
    * Properties of a grid cell polygon.
    */
    export interface CellInfo {
        /** A unique ID for the cluster that can be used with the GriddedDataSource getCellChildren methods. */
        cell_id: string;

        /* The number of pounts in the cell. */
        point_count: number;

        /* A string that abbreviates the point_count value if it's long. (for example, 4,000 becomes 4K) */
        point_count_abbreviated: string;

        /** The calculated aggregate values. */
        aggregateProperties: Record<string, number | boolean>;
    }
}

export = atlas;