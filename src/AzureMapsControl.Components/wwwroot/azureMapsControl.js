window.azureMapsControl = {
    _drawingManager: null,
    _toolbar: null,
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
    addControls: function (controlOptions) {
        if (controlOptions && controlOptions.length > 0) {
            controlOptions.forEach(controlOption => {
                switch (controlOption.type) {
                    case "compass":
                        this._map.controls.add(new atlas.control.CompassControl(), {
                            position: controlOption.position
                        });
                        break;
                    case "pitch":
                        this._map.controls.add(new atlas.control.PitchControl(), {
                            position: controlOption.position
                        });
                        break;
                    case "style":
                        this._map.controls.add(new atlas.control.StyleControl(), {
                            position: controlOption.position
                        });
                        break;
                    case "zoom":
                        this._map.controls.add(new atlas.control.ZoomControl(), {
                            position: controlOption.position
                        });
                        break;
                }
            });
        }
    },
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
                position: [drawingToolbarOptions.dragHandleStyle.position.longitude, drawingToolbarOptions.dragHandleStyle.position.latitude],
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
                position: [drawingToolbarOptions.secondaryDragHandleStyle.position.longitude, drawingToolbarOptions.secondaryDragHandleStyle.position.latitude],
                secondaryColor: drawingToolbarOptions.secondaryDragHandleStyle.secondaryColor,
                text: drawingToolbarOptions.secondaryDragHandleStyle.text,
                visible: drawingToolbarOptions.secondaryDragHandleStyle.visible
            });
        }

        this._drawingManager = new atlas.drawing.DrawingManager(this._map, drawingManagerOptions);

        if (drawingToolbarOptions.events) {
            drawingToolbarOptions.events.forEach(drawingToolbarEvent => {
                this._map.events.add(drawingToolbarEvent, this._drawingManager, e => {
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
    addHtmlMarkers: function (htmlMarkerOptions,
        eventHelper) {

        htmlMarkerOptions.forEach(htmlMarkerOption => {
            const marker = new atlas.HtmlMarker({
                anchor: htmlMarkerOption.options.anchor,
                color: htmlMarkerOption.options.color,
                draggable: htmlMarkerOption.options.draggable,
                htmlContent: htmlMarkerOption.options.htmlContent,
                pixelOffset: htmlMarkerOption.options.pixelOffset,
                position: [htmlMarkerOption.options.position.longitude, htmlMarkerOption.options.position.latitude],
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
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(event.type, {
                            markerId: marker.amc.id
                        }));
                    });
                });
            }
            this._map.markers.add(marker);
        });
    },
    addMap: function (mapId,
        authType,
        aadAppId,
        aadTenant,
        clientId,
        subscriptionKey,
        serviceOptions,
        enabledEvents,
        eventHelper) {

        if (authType === 'aad') {
            atlas.setAuthenticationOptions({
                authType: authType,
                aadAppId: aadAppId,
                aadTenant: aadTenant,
                clientId: clientId
            });
        } else if (authType === 'subscriptionKey') {
            atlas.setAuthenticationOptions({
                authType: authType,
                subscriptionKey: subscriptionKey
            });
        } else {
            atlas.setAuthenticationOptions({
                authType: authType
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
                            pixel: Array.isArray(event.pixel) ? {
                                x: event.pixel[0],
                                y: event.pixel[1]
                            } : event.pixel,
                            position: Array.isArray(event.position) ? {
                                longitude: event.position[0],
                                latitude: event.position[1]
                            } : event.position
                        }));
                    });
                }
            });

            this._mapDataEvents.forEach(value => {
                if (enabledEvents.find(eventType => {
                    return value === eventType;
                })) {
                    map.events.add(value, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, {
                            dataType: event.dataType,
                            isSourceLoaded: event.isSourceLoaded,
                            source: event.source ? {
                                id: event.source.getId()
                            } : null,
                            sourceDataType: event.sourceDataType,
                            tile: event.tile
                        }));
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
    removeHtmlMarkers: function (markerIds) {
        this._map.markers.remove(this._map.markers.getMarkers().find(marker => markerIds.indexOf(marker.amc.id) > -1));
    },
    setOptions: function (cameraOptions,
        styleOptions,
        userInteractionOptions,
        trafficOptions) {

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
                options.center = [cameraOptions.center.longitude, cameraOptions.center.latitude];
            }
            options.zoom = cameraOptions.zoom;
        }

        this._map.setCamera(options);
        this._map.setStyle(styleOptions);
        this._map.setUserInteraction(userInteractionOptions);

        if (trafficOptions) {
            this._map.setTraffic(trafficOptions);
        }
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
                options.position = [htmlMarkerOption.options.position.longitude, htmlMarkerOption.options.position.latitude];
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
    _toMapEvent: function (type, properties) {
        const result = properties || {};
        result.type = type;
        return result;
    }
};
