import * as azmaps from 'azure-maps-control';

export interface AmcControl {
    type: 'compass' | 'pitch' | 'style' | 'zoom' | 'scalebar' | 'overviewmap';
    id: string;
    position: azmaps.ControlPosition;
}