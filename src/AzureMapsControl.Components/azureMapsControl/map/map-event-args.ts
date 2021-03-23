import { EventArgs } from '../events/event';
import * as azmaps from 'azure-maps-control';

export interface MapEventArgs extends EventArgs {
    dataType?: string;
    error?: string;
    id?: string;
    isSourceLoaded?: boolean;
    layerId?: string;
    message?: string;
    pixel?: azmaps.Pixel;
    pixels?: azmaps.Pixel[];
    position?: azmaps.data.Position;
    positions?: azmaps.data.Position[];
    source?: { id: string };
    sourceDataType?: string;
    style?: string;
    tile?: azmaps.Tile;
}