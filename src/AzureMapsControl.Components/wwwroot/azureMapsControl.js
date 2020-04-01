window.azureMapsControl = {

    _maps: [],
    _mapEvents: [
        'boxzoomend',
        'boxzoomstart',
        'drag',
        'dragend',
        'dragstart',
        'idle',
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
    addMap: function (mapId,
        subscriptionKey,
        serviceOptions,
        mapEventHelper) {
        const map = new atlas.Map(mapId, {
            authOptions: {
                authType: 'subscriptionKey',
                subscriptionKey: subscriptionKey
            },
            disableTelemetry: serviceOptions.disableTelemetry,
            enableAccessibility: serviceOptions.enableAccessibility,
            refreshExpiredTiles: serviceOptions.refreshExpiredTiles
        });

        map.events.add('error', event => {
            mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(event.type, mapId, {
                error: event.error.stack
            }));
        });

        map.events.addOnce('ready', event => {
            this._maps.push({
                id: mapId,
                map: map
            });

            mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(event.type, mapId));

            this._mapEvents.forEach(value => {
                map.events.add(value, () => {
                    mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(value, mapId));
                });
            });

            this._mapMouseEvents.forEach(value => {
                map.events.add(value, event => {
                    mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(value, mapId, {
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
            });

            this._mapDataEvents.forEach(value => {
                map.events.add(value, event => {
                    mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(value, mapId, {
                        dataType: event.dataType,
                        isSourceLoaded: event.isSourceLoaded,
                        source: event.source ? {
                            id: event.source.getId()
                        } : null,
                        sourceDataType: event.sourceDataType,
                        tile: event.tile
                    }));
                });
            });

            this._mapLayerEvents.forEach(value => {
                map.events.add(value, event => {
                    mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(value, mapId, {
                        id: event.getId()
                    }));
                });
            });

            this._mapStringEvents.forEach(value => {
                map.events.add(value, event => {
                    mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(value, mapId, {
                        message: event
                    }));
                });
            });

            this._mapTouchEvents.forEach(value => {
                map.events.add(value, event => {
                    mapEventHelper.invokeMethodAsync('NotifyMapEvent', this._toMapEvent(value, map, {
                        layerId: event.layerId,
                        pixel: event.pixel,
                        pixels: event.pixels,
                        position: event.position,
                        positions: event.positions,
                        shapes: event.shapes ? event.shapes.toJson() : null
                    }));
                });
            });
        });

    },

    setOptions: function (mapId,
        cameraOptions,
        styleOptions,
        userInteractionOptions) {

        const mapEntry = this._maps.find(currentValue => {
            return currentValue.id === mapId;
        });

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
            options.center = [cameraOptions.center.longitude, cameraOptions.center.latitude];
            options.zoom = cameraOptions.zoom;
        }

        mapEntry.map.setCamera(options);
        mapEntry.map.setStyle(styleOptions);
        mapEntry.map.setUserInteraction(userInteractionOptions);
    },

    _toMapEvent: function (type, mapId, properties) {
        const result = properties || {};
        result.mapId = mapId;
        result.type = type;
        return result;
    }
};
