import { Core } from '../core/core';
import { EventHelper } from '../events/event-helper';
import { MapEventArgs } from '../map/map-event-args';

export class HtmlMarker {

    public static togglePopup(id: string,
        popupId: string,
        events: string[],
        eventHelper: EventHelper<MapEventArgs>): void {

        const map = Core.getMap();
        const popups = Core.getPopups();
        const marker = map.markers.getMarkers().find((m: any) => m.amc.id === id);
        marker.togglePopup();

        if (!popups.has(popupId)) {
            const popup = marker.getOptions().popup;
            popups.set(popupId, popup);

            events.forEach(key => {
                map.events.add(key as any, popup, () => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: key,
                        id: popupId
                    });
                });
            });
        }
    }

}