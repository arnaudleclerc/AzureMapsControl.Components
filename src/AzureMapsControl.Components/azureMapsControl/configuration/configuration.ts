import * as azmaps from 'azure-maps-control';

export interface Configuration {
    authType: azmaps.AuthenticationType;
    aadAppId: string;
    aadTenant: string;
    clientId: string;
    subscriptionKey: string;
}