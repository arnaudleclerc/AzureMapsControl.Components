import { Core } from '../core/core';

export class Layer {

    public static setOptions(mapId: string, layerId: string, options: unknown): void {
        const layer = Core.getMap(mapId).layers.getLayerById(layerId) as any;
        layer.setOptions(options);
    }

}