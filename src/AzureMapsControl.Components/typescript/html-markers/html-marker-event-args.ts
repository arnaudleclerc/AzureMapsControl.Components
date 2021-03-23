import { EventArgs } from '../events/event';
import * as azmaps from 'azure-maps-control';

export interface HtmlMarkerEventArgs extends EventArgs {
    markerId?: string;
    options?: azmaps.HtmlMarkerOptions;
}

export const toMarkerEvent = (event: azmaps.TargetedEvent, markerId: string): HtmlMarkerEventArgs => {
    const result: HtmlMarkerEventArgs = { type: event.type, markerId: markerId };
    const targetOptions = (event?.target as any)?.options as azmaps.HtmlMarkerOptions;
    if (targetOptions) {
        result.options = {
            anchor: targetOptions.anchor,
            color: targetOptions.color,
            draggable: targetOptions.draggable,
            htmlContent: targetOptions.htmlContent,
            pixelOffset: targetOptions.pixelOffset,
            position: targetOptions.position,
            secondaryColor: targetOptions.secondaryColor,
            text: targetOptions.text,
            visible: targetOptions.visible
        };
    }
    return result;
};