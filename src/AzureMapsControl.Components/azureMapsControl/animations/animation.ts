import * as azmaps from 'azure-maps-control';
import * as azanimations from 'azure-maps-control-animations';
import { Core } from '../core';

export class Animation {

    private static readonly _animations = new Map<string, azanimations.animations.PlayableAnimation>();

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

    public static moveAlongPath(animationId: string,
        lineId: string,
        lineSourceId: string,
        pinId: string,
        pinSourceId: string,
        options: azanimations.PathAnimationOptions | azanimations.MapPathAnimationOptions): void {
        const map = Core.getMap();
        const lineSource = map.sources.getById(lineSourceId) as azmaps.source.DataSource;
        const pinSource = map.sources.getById(pinSourceId) as azmaps.source.DataSource;

        const line = lineSource.getShapeById(lineId);
        const pin = pinSource.getShapeById(pinId);
        this._animations.set(
            animationId,
            azanimations.animations.moveAlongPath(line, pin, options)
        );
    }


    public static dispose(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.dispose();
            this._animations.delete(animationId);
        }
    }

    public static pause(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.pause();
        }
    }

    public static play(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.play();
        }
    }

    public static reset(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.reset();
        }
    }

    public static seek(animationId: string, progress: number): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.seek(progress);
        }
    }

    public static stop(animationId: string): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.stop();
        }
    }

    public static setOptions(animationId: string, options: azanimations.PlayableAnimationOptions): void {
        if (this._animations.has(animationId)) {
            const animation = this._animations.get(animationId);
            animation.setOptions(options);
        }
    }

}