import * as fullscreencontrol from 'azure-maps-control-fullscreen';
import { Core } from '../core/core';

export class FullscreenControl {

    public static async isFullscreenSupported(): Promise<boolean> {
        return Promise.resolve(fullscreencontrol.control.FullscreenControl.isSupported());
    }

    public static dispose(id: string): void {
        this._getFullscreenControl(id).dispose();
    }

    public static setOptions(id: string, options: fullscreencontrol.FullscreenControlOptions): void {
        this._getFullscreenControl(id).setOptions(options);
    }

    private static _getFullscreenControl(controlId: string): fullscreencontrol.control.FullscreenControl {
        return Core.getMap().controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as fullscreencontrol.control.FullscreenControl;
    }

}