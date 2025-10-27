import { Core } from '../core/core';
import { Feature, Geometry, Shape } from '../geometries/geometry';
import * as azmapsgriddeddatasource from 'azure-maps-gridded-data-source';
import * as azmaps from 'azure-maps-control';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class GriddedDatasource {

    public static getCellChildren(mapId: string, sourceId: string, cellId: string): Feature[] {
        return (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource)
            .getCellChildren(cellId)
            .map(feature => Core.getSerializableFeature(feature));
    }

    public static getGridCells(mapId: string, sourceId: string): Feature[] {
        return (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource)
            .getGridCells().features.map(feature => Core.getSerializableFeature(feature));
    }

    public static getPoints(mapId: string, sourceId: string): Feature[] {
        return (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource)
            .getPoints().features.map(feature => Core.getSerializableFeature(feature));
    }

    public static setFeatureCollectionPoints(mapId: string, sourceId: string, featureCollection: azmaps.data.FeatureCollection): void {
        (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource).setPoints(featureCollection);
    }

    public static setFeaturePoints(mapId: string, sourceId: string, features: Feature[]): void {
        const points = features.map(feature => GeometryBuilder.buildFeature(feature) as azmaps.data.Feature<azmaps.data.Point, any>);
        (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource).setPoints(points);
    }

    public static setPoints(mapId: string, sourceId: string, geometries: Geometry[]): void {
        const points = geometries.map(geometry => GeometryBuilder.buildPoint(geometry));
        (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource).setPoints(points);
    }

    public static setShapePoints(mapId: string, sourceId: string, shapes: Shape[]): void {
        const points = shapes.map(shape => GeometryBuilder.buildShape(shape));
        (Core.getMap(mapId).sources.getById(sourceId) as azmapsgriddeddatasource.source.GriddedDataSource).setPoints(points);
    }

}