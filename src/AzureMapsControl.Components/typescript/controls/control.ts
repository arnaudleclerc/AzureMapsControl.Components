import * as azmaps from 'azure-maps-control';
import * as scalebar from 'azure-maps-control-scalebar';
import * as overviewmap from 'azure-maps-control-overviewmap';
import * as geolocationcontrol from 'azure-maps-control-geolocation';

export interface Control {
    type: 'compass' | 'pitch' | 'style' | 'zoom' | 'scalebar' | 'overviewmap' | 'geolocation';
    id: string;
    position: azmaps.ControlPosition;
    options: azmaps.CompassControlOptions
    & azmaps.StyleOptions
    & azmaps.PitchControlOptions
    & azmaps.ZoomControlOptions
    & scalebar.ScaleBarControlOptions
    & overviewmap.OverviewMapOptions
    & geolocationcontrol.GeolocationControlOptions;
}