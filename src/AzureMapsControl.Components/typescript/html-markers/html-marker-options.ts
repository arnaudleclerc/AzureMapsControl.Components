import * as azmaps from 'azure-maps-control';

export interface HtmlMarkerDefinition {
    options: azmaps.HtmlMarkerOptions;
    events: string[];
    id: string;
    popupOptions: HtmlMarkerPopupOptions;
}

export interface HtmlMarkerPopupOptions {
    id: string;
    events: string[];
    options: azmaps.PopupOptions;
}