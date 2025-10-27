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
import { mapDataEvents, mapEvents, mapLayerEvents, mapMouseEvents, mapSourceEvents, mapStringEvents, mapTouchEvents } from '../map/map-events';
import { Feature, Shape } from '../geometries/geometry';
import { DataSourceEventArgs } from '../sources/datasource-event-args';
import * as griddeddatasource from 'azure-maps-gridded-data-source';

export class Core {
    private static readonly _maps: Map<string, azmaps.Map> = new Map<string, azmaps.Map>();
    private static readonly _popups: Map<string, Map<string, azmaps.Popup>> = new Map<string, Map<string, azmaps.Popup>>();

    public static getMap(mapId: string): azmaps.Map {
        if (!this._maps.has(mapId)) {
            throw new Error(`Map with ID '${mapId}' not found`);
        }
        return this._maps.get(mapId);
    }

    private static getOrCreatePopupCollection(mapId: string): Map<string, azmaps.Popup> {
        if (!this._popups.has(mapId)) {
            this._popups.set(mapId, new Map<string, azmaps.Popup>());
        }
        return this._popups.get(mapId);
    }

    public static addControls(mapId: string, controls: Control[]): void {
        const map = this.getMap(mapId);
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
            map.controls.add(mapControl, {
                position: control.position
            });
        });
    }

    public static addHtmlMarkers(mapId: string, htmlMarkerDefinitions: HtmlMarkerDefinition[],
        eventHelper: EventHelper<HtmlMarkerEventArgs>): void {
        const map = this.getMap(mapId);
        htmlMarkerDefinitions.forEach(htmlMarkerDefinition => {
            const marker = this.getHtmlMarkerFromDefinition(htmlMarkerDefinition);
            if (htmlMarkerDefinition.events) {
                htmlMarkerDefinition.events.forEach(htmlMarkerEvent => {
                    map.events.add(htmlMarkerEvent as any, marker, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', toMarkerEvent(event, (marker as any).amc.id));
                    });
                });
            }
            map.markers.add(marker);
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

    public static attachEventsToHtmlMarker(mapId: string, marker: azmaps.HtmlMarker, events: string[], eventHelper: EventHelper<HtmlMarkerEventArgs>): void {
        const map = this.getMap(mapId);
        events.forEach(htmlMarkerEvent => {
            map.events.add(htmlMarkerEvent as any, marker, event => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', toMarkerEvent(event, (marker as any).amc.id));
            });
        });
    }

    public static addLayer(mapId: string,
        id: string,
        before: string,
        layerType: LayerType,
        layerOptions: azmaps.LayerOptions,
        enabledEvents: string[],
        eventHelper: EventHelper<MapEventArgs>): void {
        const map = this.getMap(mapId);
        let layer: azmaps.layer.Layer;
        switch (layerType) {
            case 'tileLayer':
                layer = new azmaps.layer.TileLayer(layerOptions, id);
                break;

            case 'imageLayer':
                layer = new azmaps.layer.ImageLayer(layerOptions, id);
                break;

            case 'bubbleLayer':
                layer = new azmaps.layer.BubbleLayer(map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'heatmapLayer':
                layer = new azmaps.layer.HeatMapLayer(map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'lineLayer':
                layer = new azmaps.layer.LineLayer(map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'polygonExtrusionLayer':
                layer = new azmaps.layer.PolygonExtrusionLayer(map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'polygonLayer':
                layer = new azmaps.layer.PolygonLayer(map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'symbolLayer':
                layer = new azmaps.layer.SymbolLayer(map.sources.getById(layerOptions.source), id, layerOptions);
                break;
        }
        if (layer) {
            enabledEvents.forEach(layerEvent => {
                map.events.add(layerEvent as any, layer, (e: any) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: layerEvent,
                        layerId: layer.getId(),
                        pixel: e.pixel,
                        pixels: e.pixels,
                        position: e.position,
                        positions: e.positions,
                        shapes: e.shapes?.filter(shape => shape instanceof azmaps.Shape).map(shape => this.getSerializableShape(shape as azmaps.Shape)),
                        features: e.shapes?.filter(shape => shape instanceof azmaps.data.Feature || shape.type === 'Feature').map(feature => this.getSerializableFeature(feature as azmaps.data.Feature<azmaps.data.Geometry, unknown>))
                    });
                });
            });

            map.layers.add(layer, before);
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

        const mapOptions: azmaps.ServiceOptions & azmaps.StyleOptions & azmaps.UserInteractionOptions & (azmaps.CameraOptions | azmaps.CameraBoundsOptions) = {
            ...serviceOptions,
        };

        const map = new azmaps.Map(mapId, mapOptions);

        if (enabledEvents.includes('error')) {
            map.events.add('error', event => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', {
                    type: event.type,
                    error: event.error.stack
                });
            });
        }

        map.events.addOnce('ready', event => {
            this._maps.set(mapId, map);
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
                        features: mouseEvent.shapes?.filter(shape => shape instanceof azmaps.data.Feature).map(feature => this.getSerializableFeature(feature as azmaps.data.Feature<azmaps.data.Geometry, unknown>))
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

            mapSourceEvents.filter(value => enabledEvents.includes(value)).forEach(value => {
                map.events.add(value as any, (source: azmaps.source.Source) => {
                    eventHelper.invokeMethodAsync('NotifyEventAsync', {
                        type: value,
                        source: {
                            id: source.getId()
                        }
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

    public static addPopup(mapId: string, id: string, options: azmaps.PopupOptions, events: string[], eventHelper: EventHelper<MapEventArgs>): void {
        const map = this.getMap(mapId);
        const popupCollection = this.getOrCreatePopupCollection(mapId);
        
        const popup = new azmaps.Popup(options);
        popupCollection.set(id, popup);
        map.popups.add(popup);

        events.forEach(key => {
            map.events.add(key as any, popup, () => {
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

    public static addPopupWithTemplate(mapId: string,
        id: string,
        options: azmaps.PopupOptions,
        properties: { [key: string]: any },
        template: azmaps.PopupTemplate,
        events: string[],
        eventHelper: EventHelper<MapEventArgs>): void {
        options.content = azmaps.PopupTemplate.applyTemplate(Core.formatProperties(properties), template);
        this.addPopup(mapId, id, options, events, eventHelper);
    }

    public static addSource(mapId: string, id: string, options: azmaps.DataSourceOptions | azmaps.VectorTileSourceOptions | griddeddatasource.GriddedDataSourceOptions, type: SourceType, events: string[], eventHelper: EventHelper<DataSourceEventArgs>): void {
        const map = this.getMap(mapId);
        
        if (type === 'datasource') {
            const dataSource = new azmaps.source.DataSource(id, options);
            map.sources.add(dataSource);
                
            events?.forEach(event => {
                map.events.add(<any>event, dataSource, (e: azmaps.source.DataSource | azmaps.Shape[]) => {
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
            map.sources.add(new azmaps.source.VectorTileSource(id, options));
        } else if (type === 'griddeddatasource') {
            const griddedDatasource = new griddeddatasource.source.GriddedDataSource(id, options);
            map.sources.add(griddedDatasource);
        }
    }

    public static clearHtmlMarkers(mapId: string): void {
        this.getMap(mapId).markers.clear();
    }

    public static clearLayers(mapId: string): void {
        this.getMap(mapId).layers.clear();
    }

    public static clearPopups(mapId: string): void {
        this.getMap(mapId).popups.clear();
        this._popups.delete(mapId);
    }

    public static clearSources(mapId: string): void {
        this.getMap(mapId).sources.clear();
    }

    public static clearMap(mapId: string): void {
        this.getMap(mapId).clear();
        this._popups.delete(mapId);
    }

    public static getPopup(mapId: string, id: string): azmaps.Popup {
        const popupCollection = this._popups.get(mapId);
        return popupCollection?.get(id) || null;
    }

    public static getPopups(mapId: string): Map<string, azmaps.Popup> {
        return this._popups.get(mapId) || new Map<string, azmaps.Popup>();
    }

    public static removeHtmlMarkers(mapId: string, markerIds: string[]): void {
        this.getMap(mapId).markers.remove(this.getMap(mapId).markers.getMarkers().filter(marker => markerIds.includes((marker as any).amc.id)));
    }

    public static removeLayers(mapId: string, ids: string[]): void {
        this.getMap(mapId).layers.remove(ids);
    }

    public static removeSource(mapId: string, id: string): void {
        this.getMap(mapId).sources.remove(id);
    }

    public static removePopup(mapId: string, id: string): void {
        const popupCollection = this._popups.get(mapId);
        if (popupCollection?.has(id)) {
            const popup = popupCollection.get(id);
            this.getMap(mapId).popups.remove(popup);
            popupCollection.delete(id);
        }
    }

    public static setCameraOptions(mapId: string, cameraOptions: (azmaps.CameraOptions | azmaps.CameraBoundsOptions) & azmaps.AnimationOptions): void {
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

        this.getMap(mapId).setCamera(options);
    }

    public static setOptions(mapId: string, cameraOptions: azmaps.CameraOptions,
        styleOptions: azmaps.StyleOptions,
        userInteractionOptions: azmaps.UserInteractionOptions,
        trafficOptions: azmaps.TrafficOptions): void {
        this.setCameraOptions(mapId, cameraOptions);
        this.setStyleOptions(mapId, styleOptions);
        this.setUserInteraction(mapId, userInteractionOptions);
        this.setTraffic(mapId, trafficOptions);
    }

    public static setStyleOptions(mapId: string, styleOptions: azmaps.StyleOptions): void {
        this.getMap(mapId).setStyle(styleOptions);
    }

    public static setTraffic(mapId: string, trafficOptions: azmaps.TrafficOptions): void {
        this.getMap(mapId).setTraffic(trafficOptions);
    }

    public static setUserInteraction(mapId: string, userInteractionOptions: azmaps.UserInteractionOptions): void {
        this.getMap(mapId).setUserInteraction(userInteractionOptions);
    }

    public static updateHtmlMarkers(mapId: string, htmlMarkerOptions: HtmlMarkerDefinition[]): void {
        htmlMarkerOptions.forEach(htmlMarkerOption => {
            const options: azmaps.HtmlMarkerOptions = {};
            if (htmlMarkerOption.options.anchor) {
                options.anchor = htmlMarkerOption.options.anchor;
            }
            if (htmlMarkerOption.options.color) {
                options.color = htmlMarkerOption.options.color;
            }
            if (htmlMarkerOption.options.draggable !== null) {
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
            if (htmlMarkerOption.options.visible !== null) {
                options.visible = htmlMarkerOption.options.visible;
            }
            if (htmlMarkerOption.popupOptions) {
                options.popup = new azmaps.Popup(htmlMarkerOption.popupOptions.options);
            }

            this.getMap(mapId).markers.getMarkers().find(marker => (marker as any).amc.id === htmlMarkerOption.id).setOptions(options);
        });
    }

    public static createImageFromTemplate(mapId: string, imageTemplate: MapImageTemplate): void {
        this.getMap(mapId).imageSprite.createFromTemplate(
            imageTemplate.id,
            imageTemplate.templateName,
            imageTemplate.color,
            imageTemplate.secondaryColor,
            imageTemplate.scale
        );
    }

    public static setCanvasStyleProperty(mapId: string, property: string, value: string): void {
        this.getMap(mapId).getCanvas().style.setProperty(property, value);
    }

    public static setCanvasStyleProperties(mapId: string, properties: { key: string, value: string }[]): void {
        const canvas = this.getMap(mapId).getCanvas();
        properties.forEach(property => {
            canvas.style.setProperty(property.key, property.value);
        });
    }

    public static setCanvasContainerStyleProperty(mapId: string, property: string, value: string): void {
        this.getMap(mapId).getCanvasContainer().style.setProperty(property, value);
    }

    public static setCanvasContainerStyleProperties(mapId: string, properties: { key: string, value: string }[]): void {
        const canvasContainer = this.getMap(mapId).getCanvasContainer();
        properties.forEach(property => {
            canvasContainer.style.setProperty(property.key, property.value);
        });
    }

    public static getCamera(mapId: string): azmaps.CameraOptions & azmaps.CameraBoundsOptions {
        return this.getMap(mapId).getCamera();
    }

    public static getStyle(mapId: string): azmaps.StyleOptions {
        const style = this.getMap(mapId).getStyle();
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

    public static getTraffic(mapId: string): azmaps.TrafficOptions {
        const traffic = this.getMap(mapId).getTraffic();
        return <azmaps.TrafficOptions>{
            flow: traffic.flow,
            incidents: traffic.incidents
        };
    }

    public static getUserInteraction(mapId: string): azmaps.UserInteractionOptions {
        const userInteraction = this.getMap(mapId).getUserInteraction();
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
                if (typeof properties[key] === 'string' && properties[key].startsWith('azureMapsControl.datetime:')) {
                    const date = Date.parse(properties[key].replace('azureMapsControl.datetime:', ''));
                    if (!isNaN(date)) {
                        properties[key] = new Date(date);
                    }
                }
            }
        }
        return properties;
    }

    public static getSerializableFeature(feature: azmaps.data.Feature<azmaps.data.Geometry, any>): Feature {
        return {
            bbox: feature.bbox,
            geometry: feature.geometry,
            id: feature.id,
            properties: feature.properties
        } as Feature;
    }
}