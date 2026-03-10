import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';
import { Feature, Shape } from '../geometries/geometry';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class Source {

    public static addShapes(mapId: string, sourceId: string, shapes: Shape[]): void {
        const mapsShapes = shapes.map(shape => GeometryBuilder.buildShape(shape));
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.add(mapsShapes);
    }

    public static addFeatures(mapId: string, sourceId: string, features: Feature[]): void {
        const mapsFeatures = features.map(feature => GeometryBuilder.buildFeature(feature));
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.add(<any>mapsFeatures);
    }

    public static addFeatureCollection(mapId: string, sourceId: string, featureCollection: azmaps.data.FeatureCollection): void {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.add(featureCollection);
    }

    public static clear(mapId: string, sourceId: string): void {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.clear();
    }

    public static async importDataFromUrl(mapId: string, sourceId: string, url: string): Promise<void> {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        return await source.importDataFromUrl(url);
    }

    public static remove(mapId: string, sourceId: string, geometryIds: string[]): void {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.remove(geometryIds);
    }

    public static dispose(mapId: string, sourceId: string): void {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.dispose();
    }

    public static getOptions(mapId: string, sourceId: string): azmaps.DataSourceOptions {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        return source.getOptions();
    }

    public static setOptions(mapId: string, sourceId: string, options: azmaps.DataSourceOptions): void {
        const map = Core.getMap(mapId);
        const source = map.sources.getById(sourceId) as azmaps.source.DataSource;
        
        if (!source) {
            throw new Error(`Data source with Id '${sourceId}' not found in map '${mapId}'.`);
        }
        
        source.setOptions(options);
    }
}