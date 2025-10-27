import * as azdrawings from 'azure-maps-drawing-tools';
import * as azmaps from 'azure-maps-control';
import { EventHelper } from '../events/event-helper';
import { Core } from '../core/core';
import { DrawingEventArgs } from './drawing-event-args';
import { Shape } from '../geometries/geometry';
import { GeometryBuilder } from '../geometries/geometry-builder';

export class Drawing {

    private static _toolbars: Map<string, azdrawings.control.DrawingToolbar> = new Map();
    private static _drawingManagers: Map<string, azdrawings.drawing.DrawingManager> = new Map();

    public static addDrawingToolbar(mapId: string, drawingToolbarOptions: azdrawings.DrawingToolbarOptions & azdrawings.DrawingManagerOptions & { events: string[] }, eventHelper: EventHelper<DrawingEventArgs>): void {
        const toolbar = new azdrawings.control.DrawingToolbar({
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
            toolbar: toolbar
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

        const map = Core.getMap(mapId);
        const drawingManager = new azdrawings.drawing.DrawingManager(map, drawingManagerOptions);
        
        this._toolbars.set(mapId, toolbar);
        this._drawingManagers.set(mapId, drawingManager);

        if (drawingToolbarOptions.events) {
            drawingToolbarOptions.events.forEach(drawingToolbarEvent => {
                map.events.add(drawingToolbarEvent as any, drawingManager, (e: any) => {
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

    public static removeDrawingToolbar(mapId: string): void {
        const drawingManager = this._drawingManagers.get(mapId);
        const toolbar = this._toolbars.get(mapId);
        
        if (drawingManager) {
            drawingManager.dispose();
            this._drawingManagers.delete(mapId);
        }
        
        if (toolbar) {
            Core.getMap(mapId).controls.remove(toolbar);
            this._toolbars.delete(mapId);
        }
    }

    public static updateDrawingToolbar(mapId: string, drawingToolbarOptions: azdrawings.DrawingToolbarOptions): void {
        const toolbar = this._toolbars.get(mapId);
        if (toolbar) {
            toolbar.setOptions({
                buttons: drawingToolbarOptions.buttons,
                containerId: drawingToolbarOptions.containerId,
                numColumns: drawingToolbarOptions.numColumns,
                position: drawingToolbarOptions.position,
                style: drawingToolbarOptions.style,
                visible: drawingToolbarOptions.visible
            });
        }
    }

    public static addShapes(mapId: string, shapes: Shape[]): void {
        const drawingManager = this._drawingManagers.get(mapId);
        if (drawingManager) {
            const mapsShapes = shapes.map(shape => GeometryBuilder.buildShape(shape));
            drawingManager.getSource().add(mapsShapes);
        }
    }

    public static clear(mapId: string): void {
        const drawingManager = this._drawingManagers.get(mapId);
        if (drawingManager) {
            drawingManager.getSource().clear();
        }
    }
}