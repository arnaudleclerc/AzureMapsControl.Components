import { EventArgs } from '../events/event';
import { Feature } from '../geometries/geometry';


export interface GeolocationEventArgs extends EventArgs {
    code: number;
    message: string;
    feature: Feature;
}