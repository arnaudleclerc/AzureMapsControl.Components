import * as azmaps from 'azure-maps-control';
import { Core } from '../core/core';

export class Popup {

    public static close(id: string): void {
        const popup = Core.getPopup(id);
        if (popup) {
            popup.close();
        }
    }

    public static open(id: string): void {
        const popup = Core.getPopup(id);
        if (popup) {
            popup.open();
        }
    }

    public static remove(id: string): void {
        const popup = Core.getPopup(id);
        if (popup) {
            popup.remove();
            Core.removePopup(id);
        }
    }

    public static setOptions(id: string, options: azmaps.PopupOptions): void {
        const popup = Core.getPopup(id);
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
}