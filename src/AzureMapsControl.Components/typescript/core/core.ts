import * as azmaps from 'azure-maps-control';
import { Configuration } from '../configuration';
import { Extensions } from '../extensions';
import { mapDataEvents, mapEvents, mapLayerEvents, mapMouseEvents, mapStringEvents, mapTouchEvents, MapEventArgs } from '../map';
import { EventHelper } from '../events';
import { LayerType } from '../layers';
import { SourceType } from '../sources';
import { Control } from '../controls';
import * as scalebar from 'azure-maps-control-scalebar';
import * as overviewmap from 'azure-maps-control-overviewmap';
import { HtmlMarkerEventArgs, HtmlMarkerOptions, toMarkerEvent } from '../html-markers';
import { MapImageTemplate } from './map-image-template';

export class Core {
    private static readonly _popups: Map<string, azmaps.Popup> = new Map<string, azmaps.Popup>();

    private static _map: azmaps.Map;

    public static addControls(controls: Control[]): void {
        controls.forEach(control => {
            let mapControl: azmaps.Control;
            switch (control.type) {
                case 'compass':
                    mapControl = new azmaps.control.CompassControl(control.options);
                    break;
                case 'pitch':
                    mapControl = new azmaps.control.PitchControl(control.options);
                    break;
                case 'style':
                    mapControl = new azmaps.control.StyleControl(control.options);
                    break;
                case 'zoom':
                    mapControl = new azmaps.control.ZoomControl(control.options);
                    break;
                case 'scalebar':
                    mapControl = new scalebar.control.ScaleBarControl(control.options);
                    break;
                case 'overviewmap':
                    mapControl = new overviewmap.control.OverviewMap(control.options);
                    break;
            }

            (mapControl as any).amc = {
                id: control.id
            };
            this._map.controls.add(mapControl, {
                position: control.position
            });
        });
    }

    public static addHtmlMarkers(htmlMarkerOptions: HtmlMarkerOptions[], eventHelper: EventHelper<HtmlMarkerEventArgs>): void {
        htmlMarkerOptions.forEach(htmlMarkerOption => {
            const marker = this.getHtmlMarkerFromOptions(htmlMarkerOption);
            if (htmlMarkerOption.events) {
                htmlMarkerOption.events.forEach(htmlMarkerEvent => {
                    this._map.events.add(htmlMarkerEvent as any, marker, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', toMarkerEvent(event, (marker as any).amc.id));
                    });
                });
            }
            this._map.markers.add(marker);
        });
    }

    public static getHtmlMarkerFromOptions(htmlMarkerOptions: HtmlMarkerOptions): azmaps.HtmlMarker {
        const marker = new azmaps.HtmlMarker({
            anchor: htmlMarkerOptions.options.anchor,
            color: htmlMarkerOptions.options.color,
            draggable: htmlMarkerOptions.options.draggable,
            htmlContent: htmlMarkerOptions.options.htmlContent,
            pixelOffset: htmlMarkerOptions.options.pixelOffset,
            position: htmlMarkerOptions.options.position,
            secondaryColor: htmlMarkerOptions.options.secondaryColor,
            text: htmlMarkerOptions.options.text,
            visible: htmlMarkerOptions.options.visible
        });
        (marker as any).amc = {
            id: htmlMarkerOptions.id
        };
        return marker;
    }

    public static attachEventsToHtmlMarker(marker: azmaps.HtmlMarker, events: string[], eventHelper: EventHelper<HtmlMarkerEventArgs>): void {
        events.forEach(htmlMarkerEvent => {
            this._map.events.add(htmlMarkerEvent as any, marker, event => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', toMarkerEvent(event, (marker as any).amc.id));
            });
        });
    }

    public static addLayer(id: string,
        before: string,
        layerType: LayerType,
        layerOptions: azmaps.LayerOptions,
        enabledEvents: string[],
        eventHelper: EventHelper<MapEventArgs>): void {
        let layer: azmaps.layer.Layer;
        switch (layerType) {
            case 'tileLayer':
                layer = new azmaps.layer.TileLayer(layerOptions, id);
                break;

            case 'imageLayer':
                layer = new azmaps.layer.ImageLayer(layerOptions, id);
                break;

            case 'bubbleLayer':
                layer = new azmaps.layer.BubbleLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'heatmapLayer':
                layer = new azmaps.layer.HeatMapLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'lineLayer':
                layer = new azmaps.layer.LineLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'polygonExtrusionLayer':
                layer = new azmaps.layer.PolygonExtrusionLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'polygonLayer':
                layer = new azmaps.layer.PolygonLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'symbolLayer':
                layer = new azmaps.layer.SymbolLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;
        }
        if (layer) {
            enabledEvents.forEach(layerEvent => {
                this._map.events.add(layerEvent as any, layer, (e: any) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: layerEvent,
                        layerId: layer.getId(),
                        pixel: e.pixel,
                        pixels: e.pixels,
                        position: e.position,
                        positions: e.positions
                    });
                });
            });

            this._map.layers.add(layer, before);
        }
    }

    public static addMap(mapId: string,
        configuration: Configuration,
        serviceOptions: azmaps.ServiceOptions,
        enabledEvents: string[],
        eventHelper: EventHelper<MapEventArgs>): void {

        if (configuration.authType === 'aad') {
            azmaps.setAuthenticationOptions({
                authType: configuration.authType,
                aadAppId: configuration.aadAppId,
                aadTenant: configuration.aadTenant,
                clientId: configuration.clientId
            });
        } else if (configuration.authType === 'subscriptionKey') {
            azmaps.setAuthenticationOptions({
                authType: configuration.authType,
                subscriptionKey: configuration.subscriptionKey
            });
        } else {
            azmaps.setAuthenticationOptions({
                authType: configuration.authType,
                getToken: Extensions.getTokenCallback
            })
        }

        const map = new azmaps.Map(mapId, serviceOptions);

        if (enabledEvents.includes('error')) {
            map.events.add('error', event => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', {
                    type: event.type,
                    error: event.error.stack
                });
            });
        }

        map.events.addOnce('ready', event => {
            this._map = map;
            eventHelper.invokeMethodAsync('NotifyEventAsync', { type: event.type });

            mapEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, () => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', { type: value });
                });
            });

            mapMouseEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (mouseEvent: azmaps.MapMouseEvent) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: value,
                        layerId: mouseEvent.layerId,
                        pixel: mouseEvent.pixel,
                        position: mouseEvent.position
                    });
                });
            });

            mapDataEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (dataEvent: azmaps.MapDataEvent) => {
                    const mapEvent: MapEventArgs = {
                        dataType: dataEvent.dataType,
                        isSourceLoaded: dataEvent.isSourceLoaded,
                        source: dataEvent.source ? {
                            id: dataEvent.source.getId()
                        } : null,
                        sourceDataType: dataEvent.sourceDataType,
                        tile: dataEvent.tile,
                        type: value
                    };
                    if (value === 'styledata') {
                        mapEvent.style = map.getStyle().style;
                    }
                    eventHelper.invokeMethodAsync('NotifyEventAsync', mapEvent);
                });
            });

            mapLayerEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (layer: azmaps.layer.Layer) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: value,
                        id: layer.getId()
                    });
                });
            });

            mapStringEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (stringEvent: string) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: value,
                        message: stringEvent
                    });
                });
            });

            mapTouchEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (touchEvent: azmaps.MapTouchEvent) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: value,
                        layerId: touchEvent.layerId,
                        pixel: touchEvent.pixel,
                        pixels: touchEvent.pixels,
                        position: touchEvent.position,
                        positions: touchEvent.positions
                    });
                });
            });
        });
    }

    public static addPopup(id: string, options: azmaps.PopupOptions, events: string[], eventHelper: EventHelper<MapEventArgs>): void {
        const popupOptions = {
            draggable: options.draggable,
            closeButton: options.closeButton,
            content: options.content,
            fillColor: options.fillColor,
            pixelOffset: options.pixelOffset,
            position: options.position,
            showPointer: options.showPointer
        };
        const popup = new azmaps.Popup(popupOptions);
        this._popups.set(id, popup);
        this._map.popups.add(popup);

        events.forEach(key => {
            this._map.events.add(key as any, popup, () => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', {
                    type: key,
                    id: id
                });
            });
        });

        if (options.openOnAdd) {
            popup.open();
        }
    }

    public static addSource(id: string, options: azmaps.DataSourceOptions, type: SourceType): void {
        if (type === 'datasource') {
            this._map.sources.add(new azmaps.source.DataSource(id, options));
        } else if (type === 'vectortilesource') {
            this._map.sources.add(new azmaps.source.VectorTileSource(id, options));
        }
    }

    public static clearHtmlMarkers(): void {
        this._map.markers.clear();
    }

    public static clearLayers(): void {
        this._map.layers.clear();
    }

    public static clearPopups(): void {
        this._map.popups.clear();
        this._popups.clear();
    }

    public static clearSources(): void {
        this._map.sources.clear();
    }

    public static clearMap(): void {
        this._map.clear();
        this._popups.clear();
    }

    public static getMap(): azmaps.Map {
        return this._map;
    }

    public static getPopup(id: string): azmaps.Popup {
        return this._popups.has(id) ? this._popups.get(id) : null;
    }

    public static removeHtmlMarkers(markerIds: string[]): void {
        this._map.markers.remove(this._map.markers.getMarkers().find(marker => markerIds.indexOf((marker as any).amc.id) > -1));
    }

    public static removeLayers(ids: string[]): void {
        this._map.layers.remove(ids);
    }

    public static removeSource(id: string): void {
        this._map.sources.remove(id);
    }

    public static removePopup(id: string): void {
        if (this._popups.has(id)) {
            this._popups.delete(id);
        }
    }

    public static setCameraOptions(cameraOptions: azmaps.CameraOptions & azmaps.CameraBoundsOptions): void {
        const options: azmaps.CameraOptions & azmaps.CameraBoundsOptions = {
            bearing: cameraOptions.bearing,
            centerOffset: cameraOptions.centerOffset,
            duration: cameraOptions.duration,
            maxZoom: cameraOptions.maxZoom,
            minZoom: cameraOptions.minZoom,
            pitch: cameraOptions.pitch,
            type: cameraOptions.cameraType
        };

        if (cameraOptions.bounds) {
            options.bounds = cameraOptions.bounds;
            options.maxBounds = cameraOptions.maxBounds;
            options.offset = cameraOptions.offset;
            options.padding = cameraOptions.padding;
        } else {
            if (cameraOptions.center) {
                options.center = cameraOptions.center;
            }
            options.zoom = cameraOptions.zoom;
        }

        this._map.setCamera(options);
    }

    public static setOptions(cameraOptions: azmaps.CameraOptions,
        styleOptions: azmaps.StyleOptions,
        userInteractionOptions: azmaps.UserInteractionOptions,
        trafficOptions: azmaps.TrafficOptions): void {
        this.setCameraOptions(cameraOptions);
        this.setStyleOptions(styleOptions);
        this.setUserInteraction(userInteractionOptions);
        this.setTraffic(trafficOptions);
    }

    public static setStyleOptions(styleOptions: azmaps.StyleOptions): void {
        this._map.setStyle(styleOptions);
    }

    public static setTraffic(trafficOptions: azmaps.TrafficOptions): void {
        this._map.setTraffic(trafficOptions);
    }

    public static setUserInteraction(userInteractionOptions: azmaps.UserInteractionOptions): void {
        this._map.setUserInteraction(userInteractionOptions);
    }

    public static updateControl(control: Control): void {
        const mapControl = this._map.controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === control.id);
        (mapControl as overviewmap.control.OverviewMap).setOptions(control.options);
    }

    public static updateHtmlMarkers(htmlMarkerOptions: HtmlMarkerOptions[]): void {
        htmlMarkerOptions.forEach(htmlMarkerOption => {
            const options: azmaps.HtmlMarkerOptions = {};
            if (htmlMarkerOption.options.anchor) {
                options.anchor = htmlMarkerOption.options.anchor;
            }
            if (htmlMarkerOption.options.color) {
                options.color = htmlMarkerOption.options.color;
            }
            if (htmlMarkerOption.options.draggable) {
                options.draggable = htmlMarkerOption.options.draggable;
            }
            if (htmlMarkerOption.options.htmlContent) {
                options.htmlContent = htmlMarkerOption.options.htmlContent;
            }
            if (htmlMarkerOption.options.position) {
                options.position = htmlMarkerOption.options.position;
            }
            if (htmlMarkerOption.options.secondaryColor) {
                options.secondaryColor = htmlMarkerOption.options.secondaryColor;
            }
            if (htmlMarkerOption.options.text) {
                options.text = htmlMarkerOption.options.text;
            }
            if (htmlMarkerOption.options.visible) {
                options.visible = htmlMarkerOption.options.visible;
            }

            this._map.markers.getMarkers().find(marker => (marker as any).amc.id === htmlMarkerOption.id).setOptions(options);
        });
    }

    public static createImageFromTemplate(imageTemplate: MapImageTemplate): void {
        this._map.imageSprite.createFromTemplate(
            imageTemplate.id,
            imageTemplate.templateName,
            imageTemplate.color,
            imageTemplate.secondaryColor,
            imageTemplate.scale
        );
    }
}