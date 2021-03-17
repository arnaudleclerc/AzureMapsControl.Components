import * as azmaps from 'azure-maps-control';

export interface HtmlMarkerOptions {
    options: azmaps.HtmlMarkerOptions;
    events: string[];
    id: string;
}