import * as azmaps from 'azure-maps-control';
import { Configuration } from './configuration';

export class AzureMapsControl {
    private _map: azmaps.Map;

    private readonly _popups: azmaps.Popup[] = [];

    public static addMap(mapId: string,
        configuration: Configuration,
        serviceOptions: azmaps.ServiceOptions): void {

        if (configuration.authType === 'aad') {
            azmaps.setAuthenticationOptions({
                authType: configuration.authType,
                aadAppId: configuration.aadAppId,
                aadTenant: configuration.aadTenant,
                clientId: configuration.clientId
            });
        } else if (configuration.authType === 'subscriptionKey') {
            azmaps.setAuthenticationOptions({
                authType: configuration.authType,
                subscriptionKey: configuration.subscriptionKey
            });
        } else {
            // atlas.setAuthenticationOptions({
            //     authType: configuration.authType,
            //     getToken: window.azureMapsControl.extensions.getTokenCallback
            // })
        }

        const map = new azmaps.Map(mapId, serviceOptions);
    }
}