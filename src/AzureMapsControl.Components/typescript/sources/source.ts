import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';
import { Feature, Shape } from '../geometries/geometry';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class Source {

    public static addShapes(id: string, shapes: Shape[]): void {
        const mapsShapes = shapes.map(shape => GeometryBuilder.buildShape(shape));
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).add(mapsShapes);
    }

    public static addFeatures(id: string, features: Feature[]): void {
        const mapsFeatures = features.map(feature => GeometryBuilder.buildFeature(feature));
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).add(<any>mapsFeatures);
    }

    public static addFeatureCollection(id: string, featureCollection: azmaps.data.FeatureCollection): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).add(featureCollection);
    }

    public static clear(id: string): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).clear();
    }

    public static async importDataFromUrl(id: string, url: string): Promise<void> {
        return await (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).importDataFromUrl(url);
    }

    public static remove(id: string, geometryIds: string[]): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).remove(geometryIds);
    }

    public static dispose(id: string): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).dispose();
    }

    public static getOptions(id: string): azmaps.DataSourceOptions {
        return (Core.getMap().sources.getById(id) as (azmaps.source.DataSource)).getOptions();
    }

    public static setOptions(id: string, options: azmaps.DataSourceOptions): void {
        ((Core.getMap().sources.getById(id)) as (azmaps.source.DataSource)).setOptions(options);
    }

}