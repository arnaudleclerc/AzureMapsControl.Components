import * as azmaps from 'azure-maps-control';
import * as azmapsgriddeddatasource from 'azure-maps-gridded-data-source';
import { Core } from '../core/core';
import { Feature, Shape } from '../geometries/geometry';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class Source {

    public static addShapes(id: string, shapes: Shape[]): void {
        const mapsShapes = shapes.map(shape => GeometryBuilder.buildShape(shape));
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).add(mapsShapes);
    }

    public static addFeatures(id: string, features: Feature[]): void {
        const mapsFeatures = features.map(feature => GeometryBuilder.buildFeature(feature));
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).add(<any>mapsFeatures);
    }

    public static addFeatureCollection(id: string, featureCollection: azmaps.data.FeatureCollection): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).add(featureCollection);
    }

    public static clear(id: string): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).clear();
    }

    public static async importDataFromUrl(id: string, url: string): Promise<void> {
        return await (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).importDataFromUrl(url);
    }

    public static remove(id: string, geometryIds: string[]): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).remove(geometryIds);
    }

    public static dispose(id: string): void {
        (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).dispose();
    }

    public static getOptions(id: string): azmaps.DataSourceOptions | azmapsgriddeddatasource.GriddedDataSourceOptions {
        return (Core.getMap().sources.getById(id) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).getOptions();
    }

    public static setOptions(id: string, options: azmaps.DataSourceOptions | azmapsgriddeddatasource.GriddedDataSourceOptions): void {
        ((Core.getMap().sources.getById(id)) as (azmaps.source.DataSource | azmapsgriddeddatasource.source.GriddedDataSource)).setOptions(options);
    }

}