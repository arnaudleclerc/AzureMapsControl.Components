import * as azmaps from 'azure-maps-control';
import * as azanimations from 'azure-maps-control-animations';
import { Core } from '../core';

export class Animation {

    private static readonly _animations = new Map<string, azanimations.IPlayableAnimation>();

    public static snakeline(animationId: string,
        lineId: string,
        dataSourceId: string,
        options: azanimations.PathAnimationOptions | azanimations.MapPathAnimationOptions): void {
        const source = Core.getMap().sources.getById(dataSourceId) as azmaps.source.DataSource;
        const shape = source.getShapeById(lineId);
        this._animations.set(
            animationId,
            azanimations.animations.snakeline(shape, options)
        );
    }

}