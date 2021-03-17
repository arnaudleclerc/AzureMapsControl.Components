import * as azdrawings from 'azure-maps-drawing-tools';
import * as azmaps from 'azure-maps-control';
import { EventHelper } from '../events';
import { Core } from '../core';
import { DrawingEventArgs } from './drawing-event-args';

export class Drawing {

    private static _toolbar: azdrawings.control.DrawingToolbar;
    private static _drawingManager: azdrawings.drawing.DrawingManager;

    public static addDrawingToolbar(drawingToolbarOptions: azdrawings.DrawingToolbarOptions & azdrawings.DrawingManagerOptions & { events: string[] }, eventHelper: EventHelper<DrawingEventArgs>): void {
        this._toolbar = new azdrawings.control.DrawingToolbar({
            buttons: drawingToolbarOptions.buttons,
            containerId: drawingToolbarOptions.containerId,
            numColumns: drawingToolbarOptions.numColumns,
            position: drawingToolbarOptions.position,
            style: drawingToolbarOptions.style,
            visible: drawingToolbarOptions.visible
        });

        const drawingManagerOptions: azdrawings.DrawingManagerOptions = {
            freehandInterval: drawingToolbarOptions.freehandInterval,
            interactionType: drawingToolbarOptions.interactionType,
            mode: drawingToolbarOptions.mode,
            shapeDraggingEnabled: drawingToolbarOptions.shapeDraggingEnabled,
            toolbar: this._toolbar
        };

        if (drawingToolbarOptions.dragHandleStyle) {
            drawingManagerOptions.dragHandleStyle = new azmaps.HtmlMarker({
                anchor: drawingToolbarOptions.dragHandleStyle.anchor,
                color: drawingToolbarOptions.dragHandleStyle.color,
                draggable: drawingToolbarOptions.dragHandleStyle.draggable,
                htmlContent: drawingToolbarOptions.dragHandleStyle.htmlContent,
                pixelOffset: drawingToolbarOptions.dragHandleStyle.pixelOffset,
                position: drawingToolbarOptions.dragHandleStyle.position,
                secondaryColor: drawingToolbarOptions.dragHandleStyle.secondaryColor,
                text: drawingToolbarOptions.dragHandleStyle.text,
                visible: drawingToolbarOptions.dragHandleStyle.visible
            });
        }

        if (drawingToolbarOptions.secondaryDragHandleStyle) {
            drawingManagerOptions.secondaryDragHandleStyle = new azmaps.HtmlMarker({
                anchor: drawingToolbarOptions.secondaryDragHandleStyle.anchor,
                color: drawingToolbarOptions.secondaryDragHandleStyle.color,
                draggable: drawingToolbarOptions.secondaryDragHandleStyle.draggable,
                htmlContent: drawingToolbarOptions.secondaryDragHandleStyle.htmlContent,
                pixelOffset: drawingToolbarOptions.secondaryDragHandleStyle.pixelOffset,
                position: drawingToolbarOptions.secondaryDragHandleStyle.position,
                secondaryColor: drawingToolbarOptions.secondaryDragHandleStyle.secondaryColor,
                text: drawingToolbarOptions.secondaryDragHandleStyle.text,
                visible: drawingToolbarOptions.secondaryDragHandleStyle.visible
            });
        }

        const map = Core.getMap();
        this._drawingManager = new azdrawings.drawing.DrawingManager(map, drawingManagerOptions);
        if (drawingToolbarOptions.events) {
            drawingToolbarOptions.events.forEach(drawingToolbarEvent => {
                map.events.add(drawingToolbarEvent as any, this._drawingManager, (e: any) => {
                    if (drawingToolbarEvent === 'drawingmodechanged') {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', {
                            type: drawingToolbarEvent,
                            newMode: e
                        });
                    } else if (drawingToolbarEvent === 'drawingstarted') {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', { type: drawingToolbarEvent });
                    } else {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', {
                            type: drawingToolbarEvent,
                            data: e.data
                        });
                    }
                });
            });
        }
    }

    public static removeDrawingToolbar(): void {
        this._drawingManager.dispose();
        Core.getMap().controls.remove(this._toolbar);
        this._drawingManager = null;
        this._toolbar = null;
    }

    public static updateDrawingToolbar(drawingToolbarOptions: azdrawings.DrawingToolbarOptions): void {
        this._toolbar.setOptions({
            buttons: drawingToolbarOptions.buttons,
            containerId: drawingToolbarOptions.containerId,
            numColumns: drawingToolbarOptions.numColumns,
            position: drawingToolbarOptions.position,
            style: drawingToolbarOptions.style,
            visible: drawingToolbarOptions.visible
        });
    }

}