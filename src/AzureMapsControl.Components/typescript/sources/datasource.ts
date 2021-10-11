import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';
import { Shape } from '../geometries/geometry';

export class Datasource {

    public static getShapes(id: string): Shape[] {
        const shapes = (Core.getMap().sources.getById(id) as azmaps.source.DataSource).getShapes();
        return shapes?.map(shape => Core.getSerializableShape(shape));
    }

}