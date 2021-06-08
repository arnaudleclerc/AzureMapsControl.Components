import * as fullscreencontrol from 'azure-maps-control-fullscreen';
import { Core } from '../core/core';
import { EventHelper } from '../events/event-helper';

export class FullscreenControl {

    public static async isFullscreenSupported(): Promise<boolean> {
        return await Promise.resolve(fullscreencontrol.control.FullscreenControl.isSupported());
    }

    public static dispose(id: string): void {
        this._getFullscreenControl(id).dispose();
    }

    public static setOptions(id: string, options: fullscreencontrol.FullscreenControlOptions): void {
        this._getFullscreenControl(id).setOptions(options);
    }

    public static async isFullscreen(id: string): Promise<boolean> {
        return await Promise.resolve(this._getFullscreenControl(id).isFullscreen());
    }

    public static addEvents(controlId: string, events: string[], eventHelper: EventHelper<boolean>): void {
        const control = this._getFullscreenControl(controlId);
        const map = Core.getMap();

        events.forEach(event => {
            map.events.add(event as any, control, (_: any) => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', control.isFullscreen());
            });
        })
    }

    private static _getFullscreenControl(controlId: string): fullscreencontrol.control.FullscreenControl {
        return Core.getMap().controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as fullscreencontrol.control.FullscreenControl;
    }

}