window.azureMapsControl = {

    _maps: [],
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
            mapEventHelper.invokeMethodAsync('NotifyMapEvent', {
                "type": event.type,
                "error": event.error
            });
        });

        map.events.addOnce('ready', event => {
            this._maps.push({
                id: mapId,
                map: map
            });

            mapEventHelper.invokeMethodAsync('NotifyMapEvent', {
                "type": event.type
            });
        });
    },

    setOptions: function (mapId,
        cameraOptions) {

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
    }
};
