window.azureMapsControl = {
    /**
     * Drawing
     */
    drawing: {
        _drawingManager: null,
        _toolbar: null,
        addDrawingToolbar: function (drawingToolbarOptions,
            eventHelper) {

            this._toolbar = new atlas.control.DrawingToolbar({
                buttons: drawingToolbarOptions.buttons,
                containerId: drawingToolbarOptions.containerId,
                numColumns: drawingToolbarOptions.numColumns,
                position: drawingToolbarOptions.position,
                style: drawingToolbarOptions.style,
                visible: drawingToolbarOptions.visible
            });

            const drawingManagerOptions = {
                freehandInterval: drawingToolbarOptions.freehandInterval,
                interactionType: drawingToolbarOptions.interactionType,
                mode: drawingToolbarOptions.mode,
                shapeDraggingEnabled: drawingToolbarOptions.shapeDraggingEnabled,
                toolbar: this._toolbar
            };

            if (drawingToolbarOptions.dragHandleStyle) {
                drawingManagerOptions.dragHandleStyle = new atlas.HtmlMarker({
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
                drawingManagerOptions.secondaryDragHandleStyle = new atlas.HtmlMarker({
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

            const map = window.azureMapsControl.getMap();
            this._drawingManager = new atlas.drawing.DrawingManager(map, drawingManagerOptions);

            if (drawingToolbarOptions.events) {
                drawingToolbarOptions.events.forEach(drawingToolbarEvent => {
                    map.events.add(drawingToolbarEvent, this._drawingManager, e => {
                        if (drawingToolbarEvent === 'drawingmodechanged') {
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(drawingToolbarEvent, {
                                newMode: e
                            }));
                        } else if (drawingToolbarEvent === 'drawingstarted') {
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(drawingToolbarEvent));
                        } else {
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(drawingToolbarEvent, {
                                data: e.data
                            }));
                        }
                    });
                });
            }
        },
        updateDrawingToolbar: function (drawingToolbarOptions) {
            this._toolbar.setOptions({
                buttons: drawingToolbarOptions.buttons,
                containerId: drawingToolbarOptions.containerId,
                numColumns: drawingToolbarOptions.numColumns,
                position: drawingToolbarOptions.position,
                style: drawingToolbarOptions.style,
                visible: drawingToolbarOptions.visible
            });
        },
        removeDrawingToolbar: function () {
            window.azureMapsControl.getMap().controls.remove(this._toolbar);
            this._drawingManager = null;
            this._toolbar = null;
        },
        _toMapEvent: function (type, properties) {
            const result = properties || {};
            result.type = type;
            return result;
        },
    },
    /**
     * Extensions namespace
     */
    extensions: {
        getTokenCallback: function (resolve, reject, map) { }
    },
    /**
     * Private fields
     */
    _map: null,
    _mapEvents: [
        'boxzoomend',
        'boxzoomstart',
        'drag',
        'dragend',
        'dragstart',
        'idle',
        'load',
        'move',
        'moveend',
        'movestart',
        'pitch',
        'pitchend',
        'pitchstart',
        'render',
        'resize',
        'rotate',
        'rotateend',
        'tokenacquired',
        'wheel',
        'zoom',
        'zoomend',
        'zoomstart'
    ],
    _mapMouseEvents: [
        'click',
        'contextmenu',
        'dblclick',
        'mousedown',
        'mousemove',
        'mouseout',
        'mouseover',
        'mouseup'
    ],
    _mapDataEvents: [
        'data',
        'sourcedata',
        'styledata'
    ],
    _mapLayerEvents: [
        'layeradded',
        'layerremoved'
    ],
    _mapSourceEvents: [
        'sourceadded',
        'sourceremoved'
    ],
    _mapStringEvents: [
        'styleimagemissing'
    ],
    _mapTouchEvents: [
        'touchcancel',
        'touchend',
        'touchstart'
    ],
    _popups: [],
    /**
     * Controls
     */
    addControls: function (controls) {
        if (controls && controls.length > 0) {
            controls.forEach(control => {
                let mapControl;
                switch (control.type) {
                    case "compass":
                        mapControl = new atlas.control.CompassControl(control.options);
                        break;
                    case "pitch":
                        mapControl = new atlas.control.PitchControl(control.options);
                        break;
                    case "style":
                        mapControl = new atlas.control.StyleControl(control.options);
                        break;
                    case "zoom":
                        mapControl = new atlas.control.ZoomControl(control.options);
                        break;
                    case "scalebar":
                        mapControl = new atlas.control.ScaleBarControl(control.options);
                        break;
                    case "overviewmap":
                        mapControl = new atlas.control.OverviewMap(control.options);
                        break;
                }
                mapControl.amc = {
                    id: control.id
                };
                this._map.controls.add(mapControl, {
                    position: control.position
                });
            });
        }
    },
    updateControl: function (control) {
        const mapControl = this._map.controls.getControls().find(ctrl =>ctrl.amc && ctrl.amc.id === control.id);
        mapControl.setOptions(control.options);
    },
    /**
     * 
     * HTML Markers
     */
    addHtmlMarkers: function (htmlMarkerOptions,
        eventHelper) {

        htmlMarkerOptions.forEach(htmlMarkerOption => {
            const marker = new atlas.HtmlMarker({
                anchor: htmlMarkerOption.options.anchor,
                color: htmlMarkerOption.options.color,
                draggable: htmlMarkerOption.options.draggable,
                htmlContent: htmlMarkerOption.options.htmlContent,
                pixelOffset: htmlMarkerOption.options.pixelOffset,
                position: htmlMarkerOption.options.position,
                secondaryColor: htmlMarkerOption.options.secondaryColor,
                text: htmlMarkerOption.options.text,
                visible: htmlMarkerOption.options.visible
            });
            marker.amc = {
                id: htmlMarkerOption.id
            };
            if (htmlMarkerOption.events) {
                htmlMarkerOption.events.forEach(htmlMarkerEvent => {
                    this._map.events.add(htmlMarkerEvent, marker, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMarkerEvent(event, marker.amc.id));
                    });
                });
            }
            this._map.markers.add(marker);
        });
    },
    clearHtmlMarkers: function () {
        this._map.markers.clear();
    },
    removeHtmlMarkers: function (markerIds) {
        this._map.markers.remove(this._map.markers.getMarkers().find(marker => markerIds.indexOf(marker.amc.id) > -1));
    },
    updateHtmlMarkers: function (htmlMarkerOptions) {
        htmlMarkerOptions.forEach(htmlMarkerOption => {

            const options = {};
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

            this._map.markers.getMarkers().find(marker => marker.amc.id === htmlMarkerOption.id).setOptions(options);
        });
    },
    /**
     * Map
     */
    addMap: function (mapId,
        configuration,
        serviceOptions,
        enabledEvents,
        eventHelper) {

        if (configuration.authType === 'aad') {
            atlas.setAuthenticationOptions({
                authType: configuration.authType,
                aadAppId: configuration.aadAppId,
                aadTenant: configuration.aadTenant,
                clientId: configuration.clientId
            });
        } else if (configuration.authType === 'subscriptionKey') {
            atlas.setAuthenticationOptions({
                authType: configuration.authType,
                subscriptionKey: configuration.subscriptionKey
            });
        } else {
            atlas.setAuthenticationOptions({
                authType: configuration.authType,
                getToken: window.azureMapsControl.extensions.getTokenCallback
            });
        }

        const map = new atlas.Map(mapId, {
            disableTelemetry: serviceOptions.disableTelemetry,
            enableAccessibility: serviceOptions.enableAccessibility,
            refreshExpiredTiles: serviceOptions.refreshExpiredTiles
        });

        if (enabledEvents.find(eventType => {
            return eventType === 'error';
        })) {
            map.events.add('error', event => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(event.type, {
                    error: event.error.stack
                }));
            });
        }

        map.events.addOnce('ready', event => {
            this._map = map;

            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(event.type));

            this._mapEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, () => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value));
                    });
                }
            });

            this._mapMouseEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, {
                            layerId: event.layerId,
                            shapes: event.shapes,
                            pixel: event.pixel,
                            position: event.position
                        }));
                    });
                }
            });

            this._mapDataEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, event => {
                        const mapEvent = {
                            dataType: event.dataType,
                            isSourceLoaded: event.isSourceLoaded,
                            source: event.source ? {
                                id: event.source.getId()
                            } : null,
                            sourceDataType: event.sourceDataType,
                            tile: event.tile
                        };
                        if (value === 'styledata') {
                            mapEvent.style = map.getStyle().style;
                        }
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, mapEvent));
                    });
                }
            });

            this._mapLayerEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, {
                            id: event.getId()
                        }));
                    });
                }
            });

            this._mapStringEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, {
                            message: event
                        }));
                    });
                }
            });

            this._mapTouchEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, {
                            layerId: event.layerId,
                            pixel: event.pixel,
                            pixels: event.pixels,
                            position: event.position,
                            positions: event.positions,
                            shapes: event.shapes ? event.shapes.toJson() : null
                        }));
                    });
                }
            });
        });
    },
    clearMap: function () {
        this._map.clear();
        this._popups = [];
    },
    getMap: function () { return this._map; },
    setOptions: function (cameraOptions,
        styleOptions,
        userInteractionOptions,
        trafficOptions) {

        this.setCameraOptions(cameraOptions);
        this.setStyleOptions(styleOptions);
        this.setUserInteraction(userInteractionOptions);
        this.setTraffic(trafficOptions);
    },
    setTraffic: function (trafficOptions) {
        this._map.setTraffic(trafficOptions);
    },
    setUserInteraction: function (userInteractionOptions) {
        this._map.setUserInteraction(userInteractionOptions);
    },
    setStyleOptions: function (styleOptions) {
        this._map.setStyle(styleOptions);
    },
    setCameraOptions: function (cameraOptions) {
        const options = {
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
    },
    /**
     * Layers
     */
    addLayer: function (id,
        before,
        layerType,
        layerOptions,
        enabledEvents,
        eventHelper) {
        let layer;
        switch (layerType) {
            case 'tileLayer':
                layer = new atlas.layer.TileLayer(layerOptions, id);
                break;

            case 'imageLayer':
                layer = new atlas.layer.ImageLayer(layerOptions, id);
                break;

            case 'bubbleLayer':
                layer = new atlas.layer.BubbleLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'heatmapLayer':
                layer = new atlas.layer.HeatMapLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'lineLayer':
                layer = new atlas.layer.LineLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'polygonExtrusionLayer':
                layer = new atlas.layer.PolygonExtrusionLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'polygonLayer':
                layer = new atlas.layer.PolygonLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;

            case 'symbolLayer':
                layer = new atlas.layer.SymbolLayer(this._map.sources.getById(layerOptions.source), id, layerOptions);
                break;
        }
        if (layer) {
            enabledEvents.forEach(layerEvent => {
                this._addLayerEvent(layerEvent, layer, eventHelper);
            });

            this._map.layers.add(
                layer,
                before
            );
        }
    },
    clearLayers: function () {
        this._map.layers.clear()
    },
    removeLayers: function (ids) {
        this._map.layers.remove(ids);
    },
    /**
     * Sources
     */
    addSource: function (id, options, type) {
        if (type === "datasource") {
            this._map.sources.add(new atlas.source.DataSource(id, options));
        } else if (type === "vectortilesource") {
            this._map.sources.add(new atlas.source.VectorTileSource(id, options));
        }
    },
    clearSources: function () {
        this._map.sources.clear();
    },
    removeSource: function (id) {
        this._map.sources.remove(id);
    },
    dataSource_add: function (id, geometries) {
        const shapes = [];
        for (const geometry of geometries) {
            switch (geometry.type) {
                case 'Point':
                    shapes.push(new atlas.Shape(new atlas.data.Point(geometry.coordinates), geometry.id));
                    break;

                case 'LineString':
                    shapes.push(
                        new atlas.Shape(
                            new atlas.data.LineString(
                                geometry.coordinates,
                                geometry.bbox ? new atlas.data.BoundingBox(
                                    new atlas.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new atlas.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            )
                            , geometry.id)
                    );
                    break;

                case 'Polygon':
                    shapes.push(
                        new atlas.Shape(
                            new atlas.data.Polygon(
                                geometry.coordinates,
                                geometry.bbox ? new atlas.data.BoundingBox(
                                    new atlas.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new atlas.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;

                case 'MultiPoint':
                    shapes.push(
                        new atlas.Shape(
                            new atlas.data.MultiPoint(
                                geometry.coordinates,
                                geometry.bbox ? new atlas.data.BoundingBox(
                                    new atlas.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new atlas.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;

                case 'MultiLineString':
                    shapes.push(
                        new atlas.Shape(
                            new atlas.data.MultiLineString(
                                geometry.coordinates,
                                geometry.bbox ? new atlas.data.BoundingBox(
                                    new atlas.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new atlas.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;

                case 'MultiPolygon':
                    shapes.push(
                        new atlas.Shape(
                            new atlas.data.Polygon(
                                geometry.coordinates,
                                geometry.bbox ? new atlas.data.BoundingBox(
                                    new atlas.data.Position(geometry.bbox.south, geometry.bbox.west)
                                    , new atlas.data.Position(geometry.bbox.north, geometry.bbox.east)
                                ) : null
                            ), geometry.id)
                    );
                    break;
            }
        }
        this._map.sources.getById(id).add(shapes);
    },
    dataSource_clear: function (id) {
        this._map.sources.getById(id).clear();
    },
    dataSource_importDataFromUrl: function (id, url) {
        this._map.sources.getById(id).importDataFromUrl(url);
    },
    dataSource_remove: function (id, geometryIds) {
        this._map.sources.getById(id).remove(geometryIds);
    },
    /**
     * Popups 
     */
    addPopup: function (id
        , options
        , events
        , eventHelper) {
        const popupOptions = {
            draggable: options.draggable,
            closeButton: options.closeButton,
            content: options.content,
            fillColor: options.fillColor,
            pixelOffset: options.pixelOffset,
            position: options.position,
            showPointer: options.showPointer
        };
        const popup = new atlas.Popup(popupOptions);
        this._popups.push({
            id: id,
            popup: popup
        });
        this._map.popups.add(popup);

        events.forEach(key => {
            this._map.events.add(key, popup, e => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', {
                    type: key,
                    id: id
                });
            });
        });

        if (options.openOnAdd) {
            popup.open();
        }
    },
    clearPopups: function () {
        this._map.popups.clear();
        this._popups = [];
    },
    popup_open: function (id) {
        const popupEntry = this._popups.find(p => p.id === id);
        if (popupEntry) {
            popupEntry.popup.open();
        }
    },
    popup_close: function (id) {
        const popupEntry = this._popups.find(p => p.id === id);
        if (popupEntry) {
            popupEntry.popup.close();
        }
    },
    popup_remove: function (id) {
        const index = this._popups.findIndex(p => p.id === id);
        if (index > -1) {
            this._popups[index].popup.remove();
            this._popups.splice(index, 1);
        }
    },
    popup_update: function (id, options) {
        const popupEntry = this._popups.find(p => p.id === id);
        if (popupEntry) {
            const popupOptions = {
                draggable: options.draggable,
                closeButton: options.closeButton,
                content: options.content,
                fillColor: options.fillColor,
                pixelOffset: options.pixelOffset,
                position: options.position,
                showPointer: options.showPointer
            };

            popupEntry.popup.setOptions(popupOptions);
        }
    },
    /**
     * Private methods
     */
    _addLayerEvent: function (key, layer, eventHelper) {
        this._map.events.add(key, layer, e => {
            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(key, {
                layerId: layer.id,
                pixel: e.pixel,
                pixels: e.pixels,
                position: e.position,
                positions: e.positions,
                shapes: e.shapes
            }));
        });
    },
    _toMapEvent: function (type, properties) {
        const result = properties || {};
        result.type = type;
        return result;
    },
    _toMarkerEvent: function (event, markerId) {
        const result = this._toMapEvent(event.type, {
            markerId: markerId
        });
        if (event.target && event.target.options) {
            result.options = event.target.options;
        }
        return result;
    }
};