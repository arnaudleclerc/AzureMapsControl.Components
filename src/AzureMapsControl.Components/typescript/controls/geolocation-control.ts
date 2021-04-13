import { Core } from '../core/core';
import * as geolocationcontrol from 'azure-maps-control-geolocation';
import { Feature } from '../geometries/geometry';

export class GeolocationControl {

    public static getLastKnownPosition(controlId: string): Feature {
        const map = Core.getMap();
        const control = map.controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as geolocationcontrol.control.GeolocationControl;
        const position = control.getLastKnownPosition();
        return {
            geometry: position.geometry,
            properties: position.properties
        } as Feature;
    }

    public static async isGeolocationSupported(): Promise<boolean> {
        return await geolocationcontrol.control.GeolocationControl.isSupported();
    }

    public static dispose(controlId: string): void {
        const map = Core.getMap();
        const control = map.controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as geolocationcontrol.control.GeolocationControl;
        control.dispose();
    }

}