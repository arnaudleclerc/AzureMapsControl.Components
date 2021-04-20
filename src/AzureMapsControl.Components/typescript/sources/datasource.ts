import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';
import { Feature, Shape } from '../geometries/geometry';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class Datasource {

    public static addShapes(id: string, shapes: Shape[]): void {
        const mapsShapes = shapes.map(shape => GeometryBuilder.buildShape(shape));
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).add(mapsShapes);
    }

    public static addFeatures(id: string, features: Feature[]): void {
        const mapsFeatures = features.map(feature => GeometryBuilder.buildFeature(feature));
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).add(mapsFeatures);
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

    public static dispose(id: string): void {
        (Core.getMap().sources.getById(id) as azmaps.source.DataSource).dispose();
    }

}