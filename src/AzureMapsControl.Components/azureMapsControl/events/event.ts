import * as azmaps from 'azure-maps-control';

export interface EventArgs {
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
    shapes?: (azmaps.data.Feature<azmaps.data.Geometry, any> | azmaps.Shape)[];
    source?: { id: string };
    sourceDataType?: string;
    style?: string;
    tile?: azmaps.Tile;
    type?: string;
}