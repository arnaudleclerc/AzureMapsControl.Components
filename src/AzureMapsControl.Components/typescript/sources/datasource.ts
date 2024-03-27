import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';
import { Shape, Feature } from '../geometries/geometry';

export class Datasource {

    public static getShapes(id: string): Shape[] {
        const shapes = (Core.getMap().sources.getById(id) as azmaps.source.DataSource).getShapes();
        return shapes?.map(shape => Core.getSerializableShape(shape));
    }

    public static async getClusterLeaves(datasourceId: string, clusterId: number, limit: number, offset: number): Promise<(Shape | Feature)[]> {
        return new Promise(resolve => {
            (Core.getMap().sources.getById(datasourceId) as azmaps.source.DataSource).getClusterLeaves(clusterId, limit, offset).then(clusterLeaves => {

                const resultLeaves = clusterLeaves.map(leaf => {
                    if (leaf instanceof azmaps.Shape) {
                        return Core.getSerializableShape(leaf);
                    }

                    if (leaf instanceof azmaps.data.Feature) {
                        return Core.getSerializableFeature(leaf);
                    }
                });

                resolve(resultLeaves);
            });
        });
    }

    public static async getClusterExpansionZoom(datasourceId: string, clusterId: number): Promise<number> {
        return new Promise(resolve => {
            (Core.getMap().sources.getById(datasourceId) as azmaps.source.DataSource).getClusterExpansionZoom(clusterId).then(zoom => {
                resolve(zoom);
            });
        });
    }

}