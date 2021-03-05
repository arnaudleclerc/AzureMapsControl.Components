import * as azmaps from 'azure-maps-control';
import { Geometry } from './geometry';

export class GeometryBuilder {

    public static buildShape(geometry: Geometry): azmaps.Shape {
        return new azmaps.Shape(
            this.buildGeometry(geometry),
            geometry.id
        );
    }

    public static buildGeometry(geometry: Geometry): azmaps.data.Geometry {
        switch (geometry.type) {
            case 'Point':
                return new azmaps.data.Point(geometry.coordinates);
            case 'LineString':
                return new azmaps.data.LineString(
                    geometry.coordinates,
                    geometry.bbox ? new azmaps.data.BoundingBox(
                        new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                        , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                    ) : null
                );
            case 'Polygon':
                return new azmaps.data.Polygon(
                    geometry.coordinates,
                    geometry.bbox ? new azmaps.data.BoundingBox(
                        new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                        , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                    ) : null
                );
            case 'MultiPoint':
                return new azmaps.data.MultiPoint(
                    geometry.coordinates,
                    geometry.bbox ? new azmaps.data.BoundingBox(
                        new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                        , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                    ) : null
                );
            case 'MultiLineString':
                return new azmaps.data.MultiLineString(
                    geometry.coordinates,
                    geometry.bbox ? new azmaps.data.BoundingBox(
                        new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                        , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                    ) : null
                );
            case 'MultiPolygon':
                return new azmaps.data.Polygon(
                    geometry.coordinates,
                    geometry.bbox ? new azmaps.data.BoundingBox(
                        new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                        , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                    ) : null
                );
        }
    }
}