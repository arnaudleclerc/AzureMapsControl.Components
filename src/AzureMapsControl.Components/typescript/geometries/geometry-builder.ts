import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';
import { Feature, Geometry, Shape } from './geometry';

export class GeometryBuilder {

    public static buildFeature(feature: Feature): azmaps.data.Feature<azmaps.data.Geometry, any> {
        const geometry = this.buildGeometry(feature.geometry);
        return new azmaps.data.Feature(geometry, Core.formatProperties(feature.properties), feature.id,
            feature.bbox ? new azmaps.data.BoundingBox(feature.bbox) : null
        );
    }

    public static buildShape(shape: Shape): azmaps.Shape {
        return new azmaps.Shape(
            this.buildGeometry(shape.geometry),
            shape.id,
            Core.formatProperties(shape.properties)
        );
    }

    public static buildGeometry(geometry: Geometry): azmaps.data.Geometry {
        switch (geometry.type) {
            case 'Point':
                return this.buildPoint(geometry);
            case 'LineString':
                return this.buildLineString(geometry);
            case 'Polygon':
                return this.buildPolygon(geometry);
            case 'MultiPoint':
                return this.buildMultiPoint(geometry);
            case 'MultiLineString':
                return this.buildMultiLineString(geometry);
            case 'MultiPolygon':
                return this.buildMultiPolygon(geometry);
            default:
                return null;
        }
    }

    public static buildPoint(geometry: Geometry): azmaps.data.Point {
        return new azmaps.data.Point(geometry.coordinates);
    }

    public static buildLineString(geometry: Geometry): azmaps.data.LineString {
        return new azmaps.data.LineString(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(geometry.bbox) : null
        );
    }

    public static buildPolygon(geometry: Geometry): azmaps.data.Polygon {
        return new azmaps.data.Polygon(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(geometry.bbox) : null
        );
    }

    public static buildMultiPoint(geometry: Geometry): azmaps.data.MultiPoint {
        return new azmaps.data.MultiPoint(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(geometry.bbox) : null
        );
    }

    public static buildMultiLineString(geometry: Geometry): azmaps.data.MultiLineString {
        return new azmaps.data.MultiLineString(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(geometry.bbox) : null
        );
    }

    public static buildMultiPolygon(geometry: Geometry): azmaps.data.MultiPolygon {
        return new azmaps.data.MultiPolygon(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(geometry.bbox) : null
        );
    }
}