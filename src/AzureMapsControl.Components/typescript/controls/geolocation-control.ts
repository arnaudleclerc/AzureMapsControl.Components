import { Core } from '../core/core';
import * as geolocationcontrol from 'azure-maps-control-geolocation';
import { Feature } from '../geometries/geometry';

export class GeolocationControl {

    public static getLastKnownPosition(controlId: string): Feature {
        const position = this._getGeolocationControl(controlId).getLastKnownPosition();
        return {
            geometry: position.geometry,
            properties: position.properties
        } as Feature;
    }

    public static async isGeolocationSupported(): Promise<boolean> {
        return await geolocationcontrol.control.GeolocationControl.isSupported();
    }

    public static dispose(controlId: string): void {
        this._getGeolocationControl(controlId).dispose();
    }

    public static setOptions(controlId: string, options: geolocationcontrol.GeolocationControlOptions): void {
        this._getGeolocationControl(controlId).setOptions(options);
    }

    private static _getGeolocationControl(controlId: string): geolocationcontrol.control.GeolocationControl {
        return Core.getMap().controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as geolocationcontrol.control.GeolocationControl;
    }

}