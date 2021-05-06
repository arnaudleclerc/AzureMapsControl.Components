import * as azmaps from 'azure-maps-control';
import * as scalebar from 'azure-maps-control-scalebar';
import * as overviewmap from 'azure-maps-control-overviewmap';
import * as geolocationcontrol from 'azure-maps-control-geolocation';
import * as fullscreencontrol from 'azure-maps-control-fullscreen';

export interface Control {
    type: 'compass' | 'pitch' | 'style' | 'zoom' | 'scalebar' | 'overviewmap' | 'geolocation' | 'fullscreen';
    id: string;
    position: azmaps.ControlPosition;
    options: azmaps.CompassControlOptions
    & azmaps.StyleOptions
    & azmaps.PitchControlOptions
    & azmaps.ZoomControlOptions
    & scalebar.ScaleBarControlOptions
    & overviewmap.OverviewMapOptions
    & geolocationcontrol.GeolocationControlOptions
    & fullscreencontrol.FullscreenControlOptions;
}