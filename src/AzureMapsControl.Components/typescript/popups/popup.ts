import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';

export class Popup {

    public static close(mapId: string, popupId: string): void {
        const popup = Core.getPopup(mapId, popupId);
        if (popup) {
            popup.close();
        }
    }

    public static open(mapId: string, popupId: string): void {
        const popup = Core.getPopup(mapId, popupId);
        if (popup) {
            popup.open();
        }
    }

    public static remove(mapId: string, popupId: string): void {
        const popup = Core.getPopup(mapId, popupId);
        if (popup) {
            popup.remove();
            Core.removePopup(mapId, popupId);
        }
    }

    public static setOptions(mapId: string, popupId: string, options: azmaps.PopupOptions): void {
        const popup = Core.getPopup(mapId, popupId);
        if (popup) {
            const popupOptions = {
                draggable: options.draggable,
                closeButton: options.closeButton,
                content: options.content,
                fillColor: options.fillColor,
                pixelOffset: options.pixelOffset,
                position: options.position,
                showPointer: options.showPointer
            };

            popup.setOptions(popupOptions);
        }
    }

    public static applyTemplate(mapId: string, popupId: string, options: azmaps.PopupOptions, properties: { [key: string]: any }, template: azmaps.PopupTemplate): void {
        options.content = azmaps.PopupTemplate.applyTemplate(properties, template);
        this.setOptions(mapId, popupId, options);
    }
}