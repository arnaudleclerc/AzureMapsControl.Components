import * as azmaps from 'azure-maps-control';
import { Configuration } from '../configuration/configuration';
import { Extensions } from '../extensions/extensions';
import { EventHelper } from '../events/event-helper';
import { LayerType } from '../layers/layer-type';
import { SourceType } from '../sources/source-type';
import { Control } from '../controls/control';
import * as scalebar from 'azure-maps-control-scalebar';
import * as overviewmap from 'azure-maps-control-overviewmap';
import * as geolocationcontrol from 'azure-maps-control-geolocation';
import * as fullscreencontrol from 'azure-maps-control-fullscreen';
import { MapImageTemplate } from './map-image-template';
import { HtmlMarkerEventArgs, toMarkerEvent } from '../html-markers/html-marker-event-args';
import { HtmlMarkerDefinition } from '../html-markers/html-marker-options';
import { MapEventArgs } from '../map/map-event-args';
import { mapDataEvents, mapEvents, mapLayerEvents, mapMouseEvents, mapStringEvents, mapTouchEvents } from '../map/map-events';
import { Feature, Shape } from '../geometries/geometry';
import { DataSourceEventArgs } from '../sources/datasource-event-args';

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
                case 'geolocation':
                    mapControl = new geolocationcontrol.control.GeolocationControl(control.options);
                    break;
                case 'fullscreen':
                    mapControl = new fullscreencontrol.control.FullscreenControl(control.options);
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

    public static addHtmlMarkers(htmlMarkerDefinitions: HtmlMarkerDefinition[],
        eventHelper: EventHelper<HtmlMarkerEventArgs>): void {
        htmlMarkerDefinitions.forEach(htmlMarkerDefinition => {
            const marker = this.getHtmlMarkerFromDefinition(htmlMarkerDefinition);
            if (htmlMarkerDefinition.events) {
                htmlMarkerDefinition.events.forEach(htmlMarkerEvent => {
                    this._map.events.add(htmlMarkerEvent as any, marker, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', toMarkerEvent(event, (marker as any).amc.id));
                    });
                });
            }
            this._map.markers.add(marker);
        });
    }

    public static getHtmlMarkerFromDefinition(htmlMarkerOptions: HtmlMarkerDefinition): azmaps.HtmlMarker {
        const options: azmaps.HtmlMarkerOptions = {
            anchor: htmlMarkerOptions.options.anchor,
            color: htmlMarkerOptions.options.color,
            draggable: htmlMarkerOptions.options.draggable,
            htmlContent: htmlMarkerOptions.options.htmlContent,
            pixelOffset: htmlMarkerOptions.options.pixelOffset,
            position: htmlMarkerOptions.options.position,
            secondaryColor: htmlMarkerOptions.options.secondaryColor,
            text: htmlMarkerOptions.options.text,
            visible: htmlMarkerOptions.options.visible
        };

        if (htmlMarkerOptions.popupOptions) {
            options.popup = new azmaps.Popup(htmlMarkerOptions.popupOptions.options);
        }

        const marker = new azmaps.HtmlMarker(options);

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
                        positions: e.positions,
                        shapes: e.shapes?.filter(shape => shape instanceof azmaps.Shape).map(shape => this.getSerializableShape(shape as azmaps.Shape)),
                        features: e.shapes?.filter(shape => shape instanceof azmaps.data.Feature || shape.type === 'Feature').map(feature => this._getSerializableFeature(feature as azmaps.data.Feature<azmaps.data.Geometry, unknown>))
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
                subscriptionKey: configuration.subscriptionKey,
                clientId: configuration.clientId
            });
        } else {
            azmaps.setAuthenticationOptions({
                authType: configuration.authType,
                getToken: Extensions.getTokenCallback,
                clientId: configuration.clientId
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
                        position: mouseEvent.position,
                        shapes: mouseEvent.shapes?.filter(shape => shape instanceof azmaps.Shape).map(shape => this.getSerializableShape(shape as azmaps.Shape)),
                        features: mouseEvent.shapes?.filter(shape => shape instanceof azmaps.data.Feature).map(feature => this._getSerializableFeature(feature as azmaps.data.Feature<azmaps.data.Geometry, unknown>))
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
                        positions: touchEvent.positions,
                        shapes: touchEvent.shapes?.filter(shape => shape instanceof azmaps.Shape).map(shape => this.getSerializableShape(shape as azmaps.Shape))
                    });
                });
            });
        });
    }

    public static addPopup(id: string, options: azmaps.PopupOptions, events: string[], eventHelper: EventHelper<MapEventArgs>): void {
        const popup = new azmaps.Popup(options);
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

        if (options?.openOnAdd) {
            popup.open();
        }
    }

    public static addPopupWithTemplate(id: string,
        options: azmaps.PopupOptions,
        properties: { [key: string]: any },
        template: azmaps.PopupTemplate,
        events: string[],
        eventHelper: EventHelper<MapEventArgs>): void {
        options.content = azmaps.PopupTemplate.applyTemplate(Core.formatProperties(properties), template);
        this.addPopup(id, options, events, eventHelper);
    }

    public static addSource(id: string, options: azmaps.DataSourceOptions | azmaps.VectorTileSourceOptions, type: SourceType, events: string[], eventHelper: EventHelper<DataSourceEventArgs>): void {
        if (type === 'datasource') {
            const dataSource = new azmaps.source.DataSource(id, options);
            this._map.sources.add(dataSource);
            events?.forEach(event => {
                this._map.events.add(<any>event, dataSource, (e: azmaps.source.DataSource | azmaps.Shape[]) => {
                    const args: DataSourceEventArgs = {
                        type: event,
                        id
                    };
                    if (!(e instanceof azmaps.source.DataSource)) {
                        args.shapes = e.map(shape => Core.getSerializableShape(shape));
                    }

                    eventHelper.invokeMethodAsync('NotifyEventAsync', args);
                });
            });
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

    public static getPopups(): Map<string, azmaps.Popup> {
        return this._popups;
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

    public static setCameraOptions(cameraOptions: (azmaps.CameraOptions | azmaps.CameraBoundsOptions) & azmaps.AnimationOptions): void {
        const options: (azmaps.CameraOptions | azmaps.CameraBoundsOptions) & azmaps.AnimationOptions = {
            bearing: cameraOptions.bearing,
            centerOffset: cameraOptions.centerOffset,
            duration: cameraOptions.duration,
            maxZoom: cameraOptions.maxZoom,
            minZoom: cameraOptions.minZoom,
            pitch: cameraOptions.pitch,
            type: cameraOptions.type
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

    public static updateHtmlMarkers(htmlMarkerOptions: HtmlMarkerDefinition[]): void {
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
            if (htmlMarkerOption.options.pixelOffset) {
                options.pixelOffset = htmlMarkerOption.options.pixelOffset;
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
            if (htmlMarkerOption.popupOptions) {
                options.popup = new azmaps.Popup(htmlMarkerOption.popupOptions.options);
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

    public static setCanvasStyleProperty(property: string, value: string): void {
        this._map.getCanvas().style.setProperty(property, value);
    }

    public static setCanvasStyleProperties(properties: { key: string, value: string }[]): void {
        const canvas = this._map.getCanvas();
        properties.forEach(property => {
            canvas.style.setProperty(property.key, property.value);
        });
    }

    public static setCanvasContainerStyleProperty(property: string, value: string): void {
        this._map.getCanvasContainer().style.setProperty(property, value);
    }

    public static setCanvasContainerStyleProperties(properties: { key: string, value: string }[]): void {
        const canvasContainer = this._map.getCanvasContainer();
        properties.forEach(property => {
            canvasContainer.style.setProperty(property.key, property.value);
        });
    }

    public static getCamera(): azmaps.CameraOptions {
        const camera = this._map.getCamera();
        return <azmaps.CameraOptions>{
            bearing: camera.bearing,
            center: camera.center,
            centerOffset: camera.centerOffset,
            maxBounds: camera.maxBounds,
            maxZoom: camera.maxZoom,
            minZoom: camera.minZoom,
            pitch: camera.pitch,
            zoom: camera.zoom
        };
    }

    public static getStyle(): azmaps.StyleOptions {
        const style = this._map.getStyle();
        return <azmaps.StyleOptions>{
            autoResize: style.autoResize,
            language: style.language,
            light: style.light,
            preserveDrawingBuffer: style.preserveDrawingBuffer,
            renderWorldCopies: style.renderWorldCopies,
            showBuildingModels: style.showBuildingModels,
            showFeedbackLink: style.showFeedbackLink,
            showLogo: style.showLogo,
            showTileBoundaries: style.showTileBoundaries,
            style: style.style,
            view: style.view
        };
    }

    public static getTraffic(): azmaps.TrafficOptions {
        const traffic = this._map.getTraffic();
        return <azmaps.TrafficOptions>{
            flow: traffic.flow,
            incidents: traffic.incidents
        };
    }

    public static getUserInteraction(): azmaps.UserInteractionOptions {
        const userInteraction = this._map.getUserInteraction();
        return <azmaps.UserInteractionOptions>{
            boxZoomInteraction: userInteraction.boxZoomInteraction,
            dblClickZoomInteraction: userInteraction.dblClickZoomInteraction,
            dragPanInteraction: userInteraction.dragPanInteraction,
            dragRotateInteraction: userInteraction.dragRotateInteraction,
            interactive: userInteraction.interactive,
            keyboardInteraction: userInteraction.keyboardInteraction,
            scrollZoomInteraction: userInteraction.scrollZoomInteraction,
            touchInteraction: userInteraction.touchInteraction,
            wheelZoomRate: userInteraction.wheelZoomRate
        };
    }

    public static getSerializableShape(shape: azmaps.Shape): Shape {
        return {
            geometry: shape.toJson().geometry,
            id: shape.getId(),
            properties: shape.getProperties()
        } as Shape;
    }

    public static formatProperties(properties: { [key: string]: any }): { [key: string]: any } {
        if (properties) {
            for (const key in properties) {
                if (typeof properties[key] === 'string') {
                    const date = Date.parse(properties[key]);
                    if (!isNaN(date)) {
                        properties[key] = new Date(date);
                    }
                }
            }
        }
        return properties;
    }

    private static _getSerializableFeature(feature: azmaps.data.Feature<azmaps.data.Geometry, any>): Feature {
        return {
            bbox: feature.bbox ? {
                west: feature.bbox[0],
                south: feature.bbox[1],
                east: feature.bbox[2],
                north: feature.bbox[3]
            } : null,
            geometry: feature.geometry,
            id: feature.id,
            properties: feature.properties
        } as Feature;
    }
}