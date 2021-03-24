import * as azmaps from 'azure-maps-control';
import { Feature, Geometry } from './geometry';

export class GeometryBuilder {

    public static buildFeature(feature: Feature): azmaps.data.Feature<azmaps.data.Geometry, any> {
        const geometry = this.buildGeometry(feature.geometry);
        return new azmaps.data.Feature(geometry, feature.properties, feature.id,
            feature.bbox ?
                new azmaps.data.BoundingBox(
                    new azmaps.data.Position(feature.bbox.south, feature.bbox.west)
                    , new azmaps.data.Position(feature.bbox.north, feature.bbox.east)
                ) : null
        );
    }

    public static buildShape(geometry: Geometry): azmaps.Shape {
        return new azmaps.Shape(
            this.buildGeometry(geometry),
            geometry.id
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
            geometry.bbox ? new azmaps.data.BoundingBox(
                new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
            ) : null
        );
    }

    public static buildPolygon(geometry: Geometry): azmaps.data.Polygon {
        return new azmaps.data.Polygon(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(
                new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
            ) : null
        );
    }

    public static buildMultiPoint(geometry: Geometry): azmaps.data.MultiPoint {
        return new azmaps.data.MultiPoint(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(
                new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
            ) : null
        );
    }

    public static buildMultiLineString(geometry: Geometry): azmaps.data.MultiLineString {
        return new azmaps.data.MultiLineString(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(
                new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
            ) : null
        );
    }

    public static buildMultiPolygon(geometry: Geometry): azmaps.data.MultiPolygon {
        return new azmaps.data.MultiPolygon(
            geometry.coordinates,
            geometry.bbox ? new azmaps.data.BoundingBox(
                new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
            ) : null
        );
    }
}