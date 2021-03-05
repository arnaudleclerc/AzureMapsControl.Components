import * as azmaps from 'azure-maps-control';
import { Core } from '../core';
import { Geometry, GeometryBuilder } from '../geometries';

export class Datasource {

    public static add(id: string, geometries: Geometry[]): void {
        const shapes = geometries.map(geometry => GeometryBuilder.buildShape(geometry));
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