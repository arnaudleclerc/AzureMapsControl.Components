import * as azmaps from 'azure-maps-control';
import { Core } from '../core';
import { Geometry } from '../geometries';

export class Datasource {

    public static add(id: string, geometries: Geometry[]): void {
        const shapes = [];
        for (const geometry of geometries) {
            switch (geometry.type) {
                case 'Point':
                    shapes.push(new azmaps.Shape(new azmaps.data.Point(geometry.coordinates), geometry.id));
                    break;

                case 'LineString':
                    shapes.push(
                        new azmaps.Shape(
                            new azmaps.data.LineString(
                                geometry.coordinates,
                                geometry.bbox ? new azmaps.data.BoundingBox(
                                    new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            )
                            , geometry.id)
                    );
                    break;

                case 'Polygon':
                    shapes.push(
                        new azmaps.Shape(
                            new azmaps.data.Polygon(
                                geometry.coordinates,
                                geometry.bbox ? new azmaps.data.BoundingBox(
                                    new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;

                case 'MultiPoint':
                    shapes.push(
                        new azmaps.Shape(
                            new azmaps.data.MultiPoint(
                                geometry.coordinates,
                                geometry.bbox ? new azmaps.data.BoundingBox(
                                    new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;

                case 'MultiLineString':
                    shapes.push(
                        new azmaps.Shape(
                            new azmaps.data.MultiLineString(
                                geometry.coordinates,
                                geometry.bbox ? new azmaps.data.BoundingBox(
                                    new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;

                case 'MultiPolygon':
                    shapes.push(
                        new azmaps.Shape(
                            new azmaps.data.Polygon(
                                geometry.coordinates,
                                geometry.bbox ? new azmaps.data.BoundingBox(
                                    new azmaps.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new azmaps.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;
            }
        }
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).add(shapes);
    }

    public static clear(id: string): void {
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).clear();
    }

    public static importDataFromUrl(id: string, url: string): void {
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).importDataFromUrl(url);
    }

    public static remove(id: string, geometryIds: string[]): void {
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).remove(geometryIds);
    }

}