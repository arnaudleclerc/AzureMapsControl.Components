window.azureMapsControl = {
    _maps: [],
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
    addControls: function (mapId,
        controlOptions) {
        if (controlOptions && controlOptions.length > 0) {
            const map = this._findMap(mapId);

            controlOptions.forEach(controlOption => {
                switch (controlOption.type) {
                    case "compass":
                        map.controls.add(new atlas.control.CompassControl(), {
                            position: controlOption.position
                        });
                        break;
                    case "pitch":
                        map.controls.add(new atlas.control.PitchControl(), {
                            position: controlOption.position
                        });
                        break;
                    case "style":
                        map.controls.add(new atlas.control.StyleControl(), {
                            position: controlOption.position
                        });
                        break;
                    case "zoom":
                        map.controls.add(new atlas.control.ZoomControl(), {
                            position: controlOption.position
                        });
                        break;
                }
            });
        }
    },
    addHtmlMarkers: function (mapId,
        htmlMarkerOptions,
        eventHelper) {
        const map = this._findMap(mapId);

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
                    map.events.add(htmlMarkerEvent, marker, event => {
                        eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(event.type, mapId, {
                            markerId: marker.amc.id
                        }));
                    });
                });
            }
            map.markers.add(marker);
        });
    },
    addMap: function (mapId,
        subscriptionKey,
        serviceOptions,
        enabledEvents,
        eventHelper) {
        const map = new atlas.Map(mapId, {
            authOptions: {
                authType: 'subscriptionKey',
                subscriptionKey: subscriptionKey
            },
            disableTelemetry: serviceOptions.disableTelemetry,
            enableAccessibility: serviceOptions.enableAccessibility,
            refreshExpiredTiles: serviceOptions.refreshExpiredTiles
        });

        if (enabledEvents.find(eventType => {
            return eventType === 'error';
        })) {
            map.events.add('error', event => {
                eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(event.type, mapId, {
                    error: event.error.stack
                }));
            });
        }

        if (enabledEvents.find(value => {
            return value === 'ready';
        })) {
            map.events.addOnce('ready', event => {
                this._maps.push({
                    id: mapId,
                    map: map
                });

                eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(event.type, mapId));

                this._mapEvents.forEach(value => {
                    if (enabledEvents.find(eventType => {
                        return value === eventType;
                    })) {
                        map.events.add(value, () => {
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, mapId));
                        });
                    }
                });

                this._mapMouseEvents.forEach(value => {
                    if (enabledEvents.find(eventType => {
                        return value === eventType;
                    })) {
                        map.events.add(value, event => {
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, mapId, {
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
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, mapId, {
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
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, mapId, {
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
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, mapId, {
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
                            eventHelper.invokeMethodAsync('NotifyEventAsync', this._toMapEvent(value, map, {
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
        }

    },
    setOptions: function (mapId,
        cameraOptions,
        styleOptions,
        userInteractionOptions) {

        const map = this._findMap(mapId);

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

        map.setCamera(options);
        map.setStyle(styleOptions);
        map.setUserInteraction(userInteractionOptions);
    },
    updateHtmlMarkers: function (mapId,
        htmlMarkerOptions) {
        const map = this._findMap(mapId);

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

            map.markers.getMarkers().find(marker => marker.amc.id === htmlMarkerOption.id).setOptions(options);
        });
    },
    _findMap: function (mapId) {
        return this._maps.find(currentValue => {
            return currentValue.id === mapId;
        }).map;
    },
    _toMapEvent: function (type, mapId, properties) {
        const result = properties || {};
        result.mapId = mapId;
        result.type = type;
        return result;
    }
};
