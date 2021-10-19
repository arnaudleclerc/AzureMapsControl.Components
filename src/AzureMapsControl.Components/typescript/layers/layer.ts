import { Core } from '../core/core';

export class Layer {

    public static setOptions(layerId: string, options: unknown): void {
        const layer = Core.getMap().layers.getLayerById(layerId) as any;
        layer.setOptions(options);
    }

}