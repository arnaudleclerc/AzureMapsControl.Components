import * as azmaps from 'azure-maps-control';
import * as scalebar from 'azure-maps-control-scalebar';
import * as overviewmap from 'azure-maps-control-overviewmap';

export interface Control {
    type: 'compass' | 'pitch' | 'style' | 'zoom' | 'scalebar' | 'overviewmap';
    id: string;
    position: azmaps.ControlPosition;
    options: azmaps.CompassControlOptions & azmaps.StyleOptions & azmaps.PitchControlOptions & azmaps.ZoomControlOptions & scalebar.ScaleBarControlOptions & overviewmap.OverviewMapOptions;
}