import * as azmaps from 'azure-maps-control';
import { EventHelper } from '../helpers';
import { Configuration } from '../configuration';
import { Extensions } from '../extensions';
import { mapDataEvents, mapEvents, mapLayerEvents, mapMouseEvents, mapStringEvents, mapTouchEvents } from '../map';
import { EventArgs } from '../events';
import { LayerType } from '../layers';
import { SourceType } from '../sources';

export class Core {
    private static readonly _popups: Map<string, azmaps.Popup> = new Map<string, azmaps.Popup>();

    private static _map: azmaps.Map;

    public static addLayer(id: string,
        before: string,
        layerType: LayerType,
        layerOptions: azmaps.LayerOptions,
        enabledEvents: string[],
        eventHelper: EventHelper): void {
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
                        positions: e.positions,
                        shapes: e.shapes
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
        eventHelper: EventHelper): void {

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
                        shapes: mouseEvent.shapes,
                        pixel: mouseEvent.pixel,
                        position: mouseEvent.position
                    });
                });
            });

            mapDataEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (dataEvent: azmaps.MapDataEvent) => {
                    const mapEvent: EventArgs = {
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
                        positions: touchEvent.positions,
                        shapes: touchEvent.shapes ?? null
                    });
                });
            });
        });
    }

    public static addPopup(id: string, options: azmaps.PopupOptions, events: string[], eventHelper: EventHelper): void {
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
}