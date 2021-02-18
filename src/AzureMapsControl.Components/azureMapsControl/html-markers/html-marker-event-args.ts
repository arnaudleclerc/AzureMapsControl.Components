import { EventArgs } from '../events';
import * as azmaps from 'azure-maps-control';

export interface HtmlMarkerEventArgs extends EventArgs {
    markerId?: string;
    options?: azmaps.HtmlMarkerOptions;
}

export const toMarkerEvent = (event: azmaps.TargetedEvent, markerId: string): HtmlMarkerEventArgs => {
    const result: HtmlMarkerEventArgs = { type: event.type, markerId: markerId };
    if (event.target && (event.target as any).options) {
        result.options = (event.target as any).options;
    }
    return result;
};