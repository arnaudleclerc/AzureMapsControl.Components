import { Core } from '../core/core';
import { EventHelper } from '../events/event-helper';
import { MapEventArgs } from '../map/map-event-args';

export class HtmlMarker {

    public static togglePopup(mapId: string,
        markerId: string,
        popupId: string,
        events: string[],
        eventHelper: EventHelper<MapEventArgs>): void {

        const map = Core.getMap(mapId);
        const popups = Core.getPopups(mapId);
        const marker = map.markers.getMarkers().find((m: any) => m.amc.id === markerId);
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