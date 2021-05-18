import { Core } from '../core/core';
import * as geolocationcontrol from 'azure-maps-control-geolocation';
import { Feature } from '../geometries/geometry';
import { EventHelper } from '../events/event-helper';
import { GeolocationEventArgs } from './geolocation-event-args';

export class GeolocationControl {

    public static getLastKnownPosition(controlId: string): Feature {
        const position = this._getGeolocationControl(controlId).getLastKnownPosition();
        return {
            geometry: position.geometry,
            properties: Core.formatProperties(position.properties)
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

    public static addEvents(controlId: string, events: string[], eventHelper: EventHelper<GeolocationEventArgs>): void {
        const control = this._getGeolocationControl(controlId);
        const map = Core.getMap();

        events.forEach(event => {
            map.events.add(event as any, control, (args: any) => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', {
                    code: args.code,
                    message: args.message,
                    feature: {
                        bbox: args.bbox,
                        geometry: args.geometry,
                        properties: Core.formatProperties(args.properties)
                    },
                    type: event
                });
            });
        })
    }

    private static _getGeolocationControl(controlId: string): geolocationcontrol.control.GeolocationControl {
        return Core.getMap().controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as geolocationcontrol.control.GeolocationControl;
    }

}