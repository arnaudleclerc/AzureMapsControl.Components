namespace AzureMapsControl.Components.Logger
{
    using Microsoft.Extensions.Logging;

    internal enum AzureMapLogEvent
    {
        AzureMap_OnInitialized = 1000,
        AzureMap_OnAfterRenderAsync = 1001,
        AzureMap_AttachDataSourcesCallback = 1002,
        AzureMap_DrawingToolbarEvent = 1003,
        AzureMap_HtmlMarkerEventReceivedAsync = 1004,
        AzureMap_LayerEventReceivedAsync = 1005,
        AzureMap_MapEventReceivedAsync = 1006,
        AzureMap_ClearMapAsync = 1007,
        AzureMap_PopupEventReceivedAsync = 1008,
        MapService_AddMapAsync = 2000,
        AnimationService_Snakeline = 3000,
        AnimationService_MoveAlongPath = 3001,
        AnimationService_FlowingDashedLine = 3002,
        AnimationService_DropMarkers = 3003,
        AnimationService_GroupAnimations = 3004,
        AnimationService_Drop = 3005,
        AnimationService_SetCoordinates = 3006,
        AnimationService_Morph = 3007,
        AnimationService_MoveAlongRoute = 3008,
        Map_AddControlsAsync = 4000,
        Map_AddSourceAsync = 4001,
        Map_RemoveSourceAsync = 4002,
        Map_ClearSourcesAsync = 4003,
        Map_AddDrawingToolbarAsync = 4004,
        Map_UpdateDrawingToolbarAsync = 4005,
        Map_RemoveDrawingToolbarAsync = 4006,
        Map_AddHtmlMarkersAsync = 4007,
        Map_UpdateHtmlMarkersAsync = 4008,
        Map_RemoveHtmlMarkersAsync = 4009,
        Map_ClearHtmlMarkersAsync = 4010,
        Map_AddLayerAsync = 4011,
        Map_RemoveLayersAsync = 4012,
        Map_ClearLayersAsync = 4013,
        Map_SetCameraOptionsAsync = 4014,
        Map_SetStyleOptionsAsync = 4015,
        Map_SetUserInteractionAsync = 4016,
        Map_SetTrafficAsync = 4017,
        Map_AddPopupAsync = 4018,
        Map_ClearPopupsAsync = 4019,
        Map_CreateImageFromTemplate = 4020,
        OverviewMapControl_UpdateAsync = 5000,
        Popup_OpenAsync = 6000,
        Popup_CloseAsync = 6001,
        Popup_RemoveAsync = 6002,
        Popup_UpdateAsync = 6003,
        DataSource_AddAsync = 7000,
        DataSource_RemoveAsync = 7001,
        DataSource_ImportDataFromUrlAsync = 7002,
        DataSource_ClearAsync = 7003,
        HtmlMarker_TogglePopupAsync = 8000,
        GeolocationControl_GetLastKnownPositionAsync = 9000,
        GeolocationControl_DisposeAsync = 9001,
        GeolocationService_IsGeolocationSupportedAsync = 9100,
    }

    internal static class Extensions
    {
        internal static void LogAzureMapsControlTrace(this ILogger logger, AzureMapLogEvent logEvent, string message, params object[] args) => logger.LogAzureMapsControl(LogLevel.Trace, logEvent, message, args);
        internal static void LogAzureMapsControlDebug(this ILogger logger, AzureMapLogEvent logEvent, string message, params object[] args) => logger.LogAzureMapsControl(LogLevel.Debug, logEvent, message, args);
        internal static void LogAzureMapsControlInfo(this ILogger logger, AzureMapLogEvent logEvent, string message, params object[] args) => logger.LogAzureMapsControl(LogLevel.Information, logEvent, message, args);
        internal static void LogAzureMapsControl(this ILogger logger, LogLevel logLevel, AzureMapLogEvent logEvent, string message, params object[] args) => logger.Log(logLevel, new EventId((int)logEvent), message, args);
    }
}
