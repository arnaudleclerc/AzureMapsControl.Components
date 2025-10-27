import * as fullscreencontrol from 'azure-maps-control-fullscreen';
import { Core } from '../core/core';
import { EventHelper } from '../events/event-helper';

export class FullscreenControl {

    public static async isFullscreenSupported(): Promise<boolean> {
        return await Promise.resolve(fullscreencontrol.control.FullscreenControl.isSupported());
    }

    public static dispose(mapId: string, controlId: string): void {
        this._getFullscreenControl(mapId, controlId).dispose();
    }

    public static setOptions(mapId: string, controlId: string, options: fullscreencontrol.FullscreenControlOptions): void {
        this._getFullscreenControl(mapId, controlId).setOptions(options);
    }

    public static async isFullscreen(mapId: string, controlId: string): Promise<boolean> {
        return await Promise.resolve(this._getFullscreenControl(mapId, controlId).isFullscreen());
    }

    public static addEvents(mapId: string, controlId: string, events: string[], eventHelper: EventHelper<boolean>): void {
        const control = this._getFullscreenControl(mapId, controlId);
        const map = Core.getMap(mapId);

        events.forEach(event => {
            map.events.add(event as any, control, (_: any) => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', control.isFullscreen());
            });
        })
    }

    private static _getFullscreenControl(mapId: string, controlId: string): fullscreencontrol.control.FullscreenControl {
        return Core.getMap(mapId).controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as fullscreencontrol.control.FullscreenControl;
    }

}