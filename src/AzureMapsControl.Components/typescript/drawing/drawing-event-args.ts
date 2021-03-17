import { EventArgs } from '../events';
import * as azmaps from 'azure-maps-control';

export interface DrawingEventArgs extends EventArgs {
    newMode?: string;
    data?: azmaps.data.Feature<azmaps.data.Geometry, any>;
}