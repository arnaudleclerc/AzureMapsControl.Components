import { Core } from '../core/core';
import { Feature } from '../geometries/geometry';
import * as azmapsgriddeddatasource from 'azure-maps-gridded-data-source';
import * as azmaps from 'azure-maps-control';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class GriddedDataource {

    public static getCellChildren(sourceId: string, cellId: string): Feature[] {
        return (Core.getMap().sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource)
            .getCellChildren(cellId)
            .map(feature => Core.getSerializableFeature(feature));
    }

    public static getGridCells(sourceId: string): Feature[] {
        return (Core.getMap().sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource)
            .getGridCells().features.map(feature => Core.getSerializableFeature(feature));
    }

    public static getPoints(sourceId: string): Feature[] {
        return (Core.getMap().sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource)
            .getPoints().features.map(feature => Core.getSerializableFeature(feature));
    }

    public static setFeatureCollectionPoints(sourceId: string, featureCollection: azmaps.data.FeatureCollection): void {
        (Core.getMap().sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource).setPoints(featureCollection);
    }

    public static setFeaturesPoints(sourceId: string, features: Feature[]): void {
        const points = features.map(feature => GeometryBuilder.buildFeature(feature) as azmaps.data.Feature<azmaps.data.Point, any>);
        (Core.getMap().sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource).setPoints(points);
    }

}